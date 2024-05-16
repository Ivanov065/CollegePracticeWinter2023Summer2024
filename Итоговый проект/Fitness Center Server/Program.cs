using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using Fitness_Center_Server;
using Npgsql;


var tcpListener = new TcpListener(IPAddress.Any, 8888);
string connectionString = "Host=127.0.0.1;Port=5432;Database=Fitness_Center;Username=postgres;Password=BlooDMaN2001";
string message = "";
try
{
	tcpListener.Start();    // запускаем сервер
	Console.WriteLine("Сервер запущен. Ожидание подключений... ");

	while (true)
	{
		// получаем подключение в виде TcpClient
		var tcpClient = await tcpListener.AcceptTcpClientAsync();
		Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} подключён");
		// создаем новую задачу для обслуживания нового клиента
		Task.Run(async () => await ProcessClientAsync(tcpClient));

		// вместо задач можно использовать стандартный Thread
		// new Thread(async ()=>await ProcessClientAsync(tcpClient)).Start();
	}
}
finally
{
	tcpListener.Stop();
}

// обрабатываем клиент
async Task ProcessClientAsync(TcpClient tcpClient)
{
	var stream = tcpClient.GetStream();
	// буфер для входящих данных
	var response = new List<byte>();
	int bytesRead = 10;
	while (true)
	{
		// считываем данные до конечного символа
		while ((bytesRead = stream.ReadByte()) != '\n')
		{
			// добавляем в буфер
			response.Add((byte)bytesRead);
		}
		var translation = Encoding.UTF8.GetString(response.ToArray());

		// если прислан маркер окончания взаимодействия,
		// выходим из цикла и завершаем взаимодействие с клиентом
		if (translation == ((int)ServerRequests.EndConnection).ToString())
		{
			Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} добровольно заканчивает соединение");
			break;
		}

		string[] strings = translation.Split('|');

		if (strings[0] == ((int)ServerRequests.LoginUser).ToString())
		{
			Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил авторизацию пользователя");
			int user_id = 0, user_role = 0;
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql =
				"SELECT user_id FROM Passwords WHERE login = @login AND p_password = @password;";
				string sql2 = "SELECT u_role FROM Users WHERE id = @user_id;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[1]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[2]));

						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								if (reader.Read())
								{
									user_id = reader.GetInt32(0);
								}
							}
							using (NpgsqlCommand sqlcommand2 = new NpgsqlCommand(sql2, connection))
							{
								sqlcommand2.Parameters.Add(new NpgsqlParameter("@user_id", user_id));

								try
								{
									using (NpgsqlDataReader reader = sqlcommand2.ExecuteReader())
									{
										if (reader.Read())
										{
											user_role = reader.GetInt32(0);
										}
										if(user_id == 0 || user_role == 0)
										{
											message = ((int)ServerRequests.EndConnection).ToString();
										}
										else message = (int)ServerRequests.LoginUser + "|" + user_id + "|" + user_role;
									}
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
									message = "error";
								}
								connection.Close();
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadClient).ToString())
		{
			Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил загрузку профиля");
			bool is_season_ticket = true;
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{

				string sqlPathfinder = "select * from season_tickets_active where client_code = @client_id;";
				string sql =
				"select C.c_name, C.surname, C.middlename, C.adres, C.phone, C.date_b, " +
				"A.id, A.date_start, A.date_end, A.Last_visitings, " +
				"M.t_name, P.s_name, T.Price, T.Visiting_amount from client C " +
				"inner join season_tickets_active A on C.id = A.client_code " +
				"inner join season_ticket T on A.season_ticket_code = T.id " +
				"inner join trainings M on T.training_code = M.id " +
				"inner join services P on T.service_code = P.id  " +
				"where C.id = @client_id;";
				try
				{
					connection.Open();

					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sqlPathfinder, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", Convert.ToInt32(strings[1])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								if (!reader.Read())
								{
									is_season_ticket = false;
									sql = "select c_name, surname, middlename, adres, phone, date_b " +
										"from client where id = @client_id;";
								}
								else
								{
									message = "0";
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}

					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", Convert.ToInt32(strings[1])));

						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								if (reader.Read())
								{
									Client client = new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2),
									reader.GetString(3), reader.GetString(4), DateOnly.FromDateTime(reader.GetDateTime(5)));

									message = (int)ServerRequests.LoadClient + "|" + client.Name + "|" + client.Surname +
										"|" + client.Middlename + "|" + client.Adres + "|" + client.Phone + "|" + client.Date.ToString();

									if (is_season_ticket) 
									{
										ClientSeasonTicket clientSeasonTicket = new ClientSeasonTicket(reader.GetInt32(6),
										DateOnly.FromDateTime(reader.GetDateTime(7)), DateOnly.FromDateTime(reader.GetDateTime(8)),
										reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetDouble(12), reader.GetInt32(13));

										message += "|" + clientSeasonTicket.TicketId + "|" + clientSeasonTicket.DateStart.ToString() + "|" +
										clientSeasonTicket.DateEnd.ToString() + "|" + clientSeasonTicket.LastVisits + "|" +
										clientSeasonTicket.TrainingName + "|" + clientSeasonTicket.ServiceName + "|" +
										clientSeasonTicket.Price + "|" + clientSeasonTicket.VisitingAmount;

										message = message.Insert(2,"1|");
									}
									else message = message.Insert(2, "0|");
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadTickets).ToString())
		{
			Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил загрузку абонементов");
			int counter = 0;

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{

				
				string sql = "select A.id, C.t_name, K.s_name, A.price, A.visiting_amount, " +
					"A.days_active from season_ticket A inner join trainings C on " +
					"A.training_code = C.id inner join services K on A.service_code = K.id;";
				try
				{
					connection.Open();

					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadTickets + "|"; 
								while (reader.Read())
								{
									message += reader.GetInt32(0) + "|" + reader.GetString(1) + "|" +
										reader.GetString(2) + "|" + reader.GetDouble(3) + "|" + reader.GetInt32(4) 
										+ "|" + reader.GetInt32(5) + "|";
									counter++;
								}

								if(counter == 0)
								{
									message += counter;
								}
								else
								{
									message = message.Remove(message.Length - 1);
									message = message.Insert(2, counter + "|");
								}

							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadSchedule).ToString())
		{
			Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил загрузку расписания");
			int counter = 0;

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select C.id, C.time_start, C.time_end, M.c_name, M.surname, A.s_name, T.t_name from schedule C " +
							"inner join Employees M on M.id = C.emp_code " +
							"inner join Services A on A.id = C.service_code " +
							"inner join Trainings T on T.id = C.training_code " +
							"where C.date_conduction = @date";
				try
				{
					connection.Open();

					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(
							new NpgsqlParameter("@date", DateOnly.FromDateTime(Convert.ToDateTime(strings[1]))));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadSchedule + "|";
								while (reader.Read())
								{
									message += reader.GetInt32(0) + "|" + reader.GetTimeSpan(1) + "|" +
									reader.GetTimeSpan(2) + "|" + reader.GetString(3) + "|" + 
									reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|";
									counter++;
								}

								if(counter == 0)
								{
									message += counter;
								}
								else
								{
									message = message.Insert(2, counter + "|");
									message = message.Remove(message.Length - 1);
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadEmployee).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку профиля");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select A.c_name, A.surname, A.middlename, A.adres, A.phone, A.date_b, C.role_name, A.salary " +
					"from employees A inner join roles C on C.id = A.e_role where A.id = @emp_id;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@emp_id", Convert.ToInt32(strings[1])));

						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								if (reader.Read())
								{
									message += (int)ServerRequests.LoadSchedule + "|" + reader.GetString(0) + "|"
									+ reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|"
									+ reader.GetString(4) + "|" + DateOnly.FromDateTime(reader.GetDateTime(5)).ToString() + "|"
									+ reader.GetString(6) + "|" + reader.GetInt32(7);
								}
								else
								{
									message = "0";
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadTrainerSchedule).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку расписания");
			int counter = 0;

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select C.id, C.time_start, C.time_end, M.c_name, M.surname, A.s_name, T.t_name from schedule C " +
							"inner join Employees M on M.id = C.emp_code " +
							"inner join Services A on A.id = C.service_code " +
							"inner join Trainings T on T.id = C.training_code " +
							"where C.date_conduction = @date and C.emp_code = @trainer_code " + 
							"order by C.Time_start ASC";
				try
				{
					connection.Open();

					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(
							new NpgsqlParameter("@date", DateOnly.FromDateTime(Convert.ToDateTime(strings[1]))));
						sqlcommand.Parameters.Add(
							new NpgsqlParameter("@trainer_code", Convert.ToInt32(strings[2])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadTrainerSchedule + "|";
								while (reader.Read())
								{
									message += reader.GetInt32(0) + "|" + reader.GetTimeSpan(1) + "|" +
									reader.GetTimeSpan(2) + "|" + reader.GetString(3) + "|" +
									reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|";
									counter++;
								}
								message = message.Remove(message.Length - 1);
								if (message.Length > 2) message = message.Insert(2, counter + "|");
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadTrainers).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку тренеров");
			int counter = 0;

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Select c_name, surname, middlename, date_b, adres, phone, salary" +
					" from employees where e_role = (Select id from Roles where role_name = 'Тренер');";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadTrainers + "|";
								while (reader.Read())
								{
									message += reader.GetString(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" +
									DateOnly.FromDateTime(reader.GetDateTime(3)) + "|" + reader.GetString(4)
									+ "|" + reader.GetString(5) + "|" + reader.GetInt32(6) + "|";
									counter++;
								}
								message = message.Remove(message.Length - 1);
								if (message.Length > 2) message = message.Insert(2, counter + "|");
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadClients).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку клиентов клуба");
			int counter = 0;

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select id, c_name, surname, middlename, date_b, adres, phone from client " + 
				"order by c_name ASC, surname ASC, middlename ASC; ";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadClients + "|";
								while (reader.Read())
								{
									message += reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2)
									+ "|" + reader.GetString(3) + "|" +
									DateOnly.FromDateTime(reader.GetDateTime(4)) + "|" + reader.GetString(5)
									+ "|" + reader.GetString(6) + "|";
									counter++;
								}
								message = message.Remove(message.Length - 1);
								if (message.Length > 2) message = message.Insert(2, counter + "|");
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadActiveTicket).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку активного абонемента клиента");

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select A.id, A.season_ticket_code, A.client_code, A.date_start, A.date_end, A.last_visitings, T.t_name, P.s_name" +
					" from season_tickets_active A inner join season_ticket C on C.id = A.season_ticket_code " +
					"inner join trainings T on T.id = C.training_code inner join services P on P.id = C.service_code " +
					"where client_code = @client_id;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", Convert.ToInt32(strings[1])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								if (reader.Read())
								{
									message += (int)ServerRequests.LoadActiveTicket + "|1|" + reader.GetInt32(0) + "|" + 
									reader.GetInt32(1) + "|" + reader.GetInt32(2) + "|" + DateOnly.FromDateTime(reader.GetDateTime(3)) + 
									"|" + DateOnly.FromDateTime(reader.GetDateTime(4)) + "|" + reader.GetInt32(5) + "|" + reader.GetString(6) +
									"|" + reader.GetString(7);
								}
								else
								{
									message += (int)ServerRequests.LoadActiveTicket + "|0";
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadManageTicketsInfo).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку информации для работы с абонементами");
			int counter_train = 0, counter_serv = 0;
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select t_name from trainings;";
				string sql2 = "select s_name from services;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadManageTicketsInfo + "|";
								while (reader.Read())
								{
									message += reader.GetString(0) + "|";
									counter_train++;
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql2, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								while (reader.Read())
								{
									message += reader.GetString(0) + "|";
									counter_serv++;
								}
								message = message.Insert(3,counter_train + "|" + counter_serv + "|");
								message = message.Remove(message.Length -1);
							} 
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadSingleTicket).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку одного абонемента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Select T.t_name, C.s_name, A.price, A.visiting_amount, A.days_active from season_ticket A " +
					"inner join services C on C.id = A.service_code " +
					"inner join trainings T on T.id = A.training_code where A.id = @ticket_id;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@ticket_id", Convert.ToInt32(strings[1])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadSingleTicket + "|";
								if (reader.Read())
								{
									message += reader.GetString(0) + "|" + reader.GetString(1) + "|" + reader.GetDouble(2) +
									"|" + reader.GetInt32(3) + "|" + reader.GetInt32(4);
								}
								else
								{
									message = "0" ;
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.DeleteTicket).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил удаление абонемента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Delete from season_ticket where id = @ticket_id";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@ticket_id", Convert.ToInt32(strings[1])));
						try
						{
							message = ((int)ServerRequests.DeleteTicket).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.AddTicket).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил добавление абонемента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Insert into season_ticket(training_code, service_code, price, visiting_amount, days_active) " +
					"values((select id from trainings where t_name = @train)," +
					" (select id from services where s_name = @service)," +
					" @price," +
					" @visits," +
					" @days)";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@train", strings[1]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@service", strings[2]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@price", Convert.ToDouble(strings[3])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@visits", Convert.ToInt32(strings[4])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@days", Convert.ToInt32(strings[5])));
						try
						{
							message = ((int)ServerRequests.AddTicket).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.UpdateTicket).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил обновление абонемента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Update season_ticket set " +
					"service_code = (select id from services where s_name = @service), " +
					"training_code = (select id from trainings where t_name = @train), " +
					"price = @price," +
					"visiting_amount = @visits," +
					"days_active = @days where id = @ticket_id";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@train", strings[1]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@service", strings[2]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@price", Convert.ToDouble(strings[3])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@visits", Convert.ToInt32(strings[4])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@days", Convert.ToInt32(strings[5])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@ticket_id", Convert.ToInt32(strings[6])));
						try
						{
							message = ((int)ServerRequests.UpdateTicket).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadManageScheduleInfo).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку информации для работы с расписанием");
			int counter_training = 0, counter_serv = 0, counter_trainers = 0;
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select t_name from trainings;";
				string sql2 = "select s_name from services;";
				string sql3 = "select c_name, surname, middlename from employees " +
					"where e_role = (select id from roles where role_name = 'Тренер');";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadManageScheduleInfo + "|";
								while (reader.Read())
								{
									message += reader.GetString(0) + "|";
									counter_training++;
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql2, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								while (reader.Read())
								{
									message += reader.GetString(0) + "|";
									counter_serv++;
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql3, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								while (reader.Read())
								{
									message += reader.GetString(0) + "|" + reader.GetString(1)
										+ "|" + reader.GetString(2) + "|";
									counter_trainers++;
								}
								message = message.Insert(3, counter_training + "|" + counter_serv + "|" + counter_trainers +  "|");
								message.Remove(message.Length - 1);
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadSingleSchedule).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку одной записи расписания");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select C.date_conduction, C.time_start, C.time_end, " +
							"M.c_name, M.surname, M.middlename, A.s_name, T.t_name from schedule C " +
							"inner join services A on A.id = C.service_code " +
							"inner join trainings T on T.id = C.training_code " +
							"inner join employees M on M.id = C.emp_code " +
							"where C.id = @schedule_id;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@schedule_id", Convert.ToInt32(strings[1])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadSingleSchedule + "|";
								if (reader.Read())
								{
									message += DateOnly.FromDateTime(reader.GetDateTime(0)) + "|"
									+ reader.GetTimeSpan(1) + "|" + reader.GetTimeSpan(2) + "|"
									+ reader.GetString(3) + "|" + reader.GetString(4) + "|" + reader.GetString(5) + "|"
									+ reader.GetString(6) + "|" + reader.GetString(7);
								}
								else
								{
									message = "0";
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.DeleteSchedule).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил удаление расписания");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Delete from schedule where id = @schedule_id";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@schedule_id", Convert.ToInt32(strings[1])));
						try
						{
							message = ((int)ServerRequests.DeleteSchedule).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.AddSchedule).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил добавление расписания");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Insert into schedule(date_conduction, time_start, time_end, emp_code, service_code, training_code) " +
					"values (@date_cond, @time_start, @time_end, " +
					"(select id from employees where e_role = (select id from roles where role_name = 'Тренер')" +
					" and c_name = @name and surname = @surname and middlename = @middlename)," +
					"(select id from services where s_name = @service_name), " +
					"(select id from trainings where t_name = @training_name));";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@date_cond", Convert.ToDateTime(strings[1])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@time_start", Convert.ToDateTime(strings[2]).TimeOfDay));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@time_end", Convert.ToDateTime(strings[3]).TimeOfDay));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[4]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[5]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[6]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@service_name", strings[7]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@training_name", strings[8]));
						try
						{
							message = ((int)ServerRequests.AddSchedule).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.UpdateSchedule).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил обновление расписания");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Update schedule set " +
					"date_conduction = @date_cond, " +
					"time_start = @time_start, " +
					"time_end = @time_end, " +
					"emp_code = (select id from employees where" +
					" e_role = (select id from roles where role_name = 'Тренер')" +
					" and c_name = @name and surname = @surname and middlename = @middlename), " +
					"service_code = (select id from services where s_name = @service_name), " +
					"training_code = (select id from trainings where t_name = @training_name) " +
					"where id = @schedule_id";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@date_cond", Convert.ToDateTime(strings[1])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@time_start", Convert.ToDateTime(strings[2]).TimeOfDay));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@time_end", Convert.ToDateTime(strings[3]).TimeOfDay));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[4]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[5]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[6]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@service_name", strings[7]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@training_name", strings[8]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@schedule_id", Convert.ToInt32(strings[9])));
						try
						{
							message = ((int)ServerRequests.UpdateSchedule).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadRoles).ToString())
		{
			Console.WriteLine($"Клиент-Администратор {tcpClient.Client.RemoteEndPoint} запросил загрузку ролей");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select role_name from Roles;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								int counter = 0;
								message += (int)ServerRequests.LoadRoles + "|";
								while (reader.Read())
								{
									message += reader.GetString(0) + "|";
									counter++;
								}
								if(counter == 0) message = "0";
								else
								{
									message = message.Insert(3, counter + "|");
									message = message.Remove(message.Length - 1);
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadOnlyClient).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил загрузку клиента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "select C.c_name, C.surname, C.middlename, C.adres, C.phone, C.date_b, P.login, P.p_password from client C " +
					"inner join Users on Users.id = C.id " +
					"inner join Passwords P on Users.id = P.user_id " +
					"where C.id = @client_id;";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", Convert.ToInt32(strings[1])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								message += (int)ServerRequests.LoadOnlyClient + "|";
								if (reader.Read())
								{
									message += reader.GetString(0) + "|" + reader.GetString(1) + "|" +
										reader.GetString(2) + "|" + reader.GetString(3) + "|" + reader.GetString(4) + "|" +
										DateOnly.FromDateTime(reader.GetDateTime(5)) +
										"|" + reader.GetString(6) + "|" + reader.GetString(7);
								}
								else
								{
									message = "0";
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.LoadUser).ToString())
		{
			Console.WriteLine($"Клиент-Администратор {tcpClient.Client.RemoteEndPoint} запросил загрузку пользователя");

			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sqlPathfinder = "Select u_role from Users where id = @user_id;";
				
				int user_role = 0;
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sqlPathfinder, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@user_id", Convert.ToInt32(strings[1])));
						try
						{
							using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
							{
								if (reader.Read())
								{
									user_role = reader.GetInt32(0);
								}
								else
								{
									message = "0";
								}
							}

						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
					if (user_role != 0)
					{
						if (user_role == (int)Roles.Client)
						{
							string sql = "select C.c_name, C.surname, C.middlename, C.adres," +
							" C.phone, C.date_b, P.login, P.p_password, A.role_name from client C " +
							"inner join Users on Users.id = C.id " +
							"inner join Passwords P on Users.id = P.user_id " +
							"inner join Roles A on A.id = Users.u_role " +
							"where C.id = @user_id;";

							using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
							{
								sqlcommand.Parameters.Add(new NpgsqlParameter("@user_id", Convert.ToInt32(strings[1])));
								try
								{
									using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
									{
										message += (int)ServerRequests.LoadUser + "|";
										if (reader.Read())
										{
											message += reader.GetString(0) + "|" + reader.GetString(1) + "|" +
												reader.GetString(2) + "|" + reader.GetString(3) + "|" + reader.GetString(4) + "|" +
												DateOnly.FromDateTime(reader.GetDateTime(5)) + "|"
												+ reader.GetString(6) + "|" + reader.GetString(7) + "|" + reader.GetString(8);
										}
										else
										{
											message = "0";
										}
									}

								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
									message = "error";
									connection.Close();
								}
							}
						}
						else
						{
							string sql = "select C.c_name, C.surname, C.middlename, C.adres," +
							" C.phone, C.date_b, P.login, P.p_password, A.role_name, C.salary from employees C " +
							"inner join Users on Users.id = C.id " +
							"inner join Passwords P on Users.id = P.user_id " +
							"inner join Roles A on A.id = Users.u_role " +
							"where C.id = @user_id;";

							using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
							{
								sqlcommand.Parameters.Add(new NpgsqlParameter("@user_id", Convert.ToInt32(strings[1])));
								try
								{
									using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
									{
										message += (int)ServerRequests.LoadUser + "|";
										if (reader.Read())
										{
											message += reader.GetString(0) + "|" + reader.GetString(1) + "|" +
												reader.GetString(2) + "|" + reader.GetString(3) + "|" + reader.GetString(4) + "|" +
												DateOnly.FromDateTime(reader.GetDateTime(5)) + "|"
												+ reader.GetString(6) + "|" + reader.GetString(7) + "|" + reader.GetString(8) + "|" +
												reader.GetInt32(9);
										}
										else
										{
											message = "0";
										}
									}

								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
									message = "error";
									connection.Close();
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.UpdateClient).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил обновление клиента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "update Client set c_name = @name, surname = @surname, middlename = @middlename, " +
					"adres = @adres, phone = @phone, date_b = @birth where id = @client_id;" +
				 "update Passwords set login = @login, p_password = @password " +
					"where user_id = @client_id";

				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[1]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[2]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[3]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@adres",strings[4]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@phone", strings[5]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@birth", Convert.ToDateTime(strings[6])));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[7]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[8]));
						sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", Convert.ToInt32(strings[9])));

						try
						{
							message = ((int)ServerRequests.UpdateClient).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 2) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.AddClient).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил добавление клиента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Insert into Users(u_role)" +
					" values ((select id from Roles where role_name = 'Клиент'));";

				try
				{
					connection.Open();

					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						try
						{
							message = ((int)ServerRequests.AddClient).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
					if (message != "error" || message != "0")
					{
						sql = "select Users.id from Users where u_role = (select id from Roles where role_name = 'Клиент') " +
							"and Not exists(select Passwords.id from Passwords where Passwords.id = Users.id);";
						int new_user_code = 0;
						using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
						{
							try
							{
								using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
								{
									if (reader.Read())
									{
										new_user_code = reader.GetInt32(0);
									}
									else
									{
										message = "0";
									}
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
								message = "error";
								connection.Close();
							}
						}
						if (new_user_code != 0)
						{
							sql = "insert into client values " +
								"(@client_id, @name, @surname, @middlename, @adres, @phone, @date_b);" +
							"insert into passwords(login, p_password, user_id) values " +
								"(@login, @password, @client_id);";
							using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
							{
								sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[1]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[2]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[3]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@adres", strings[4]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@phone", strings[5]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@date_b", Convert.ToDateTime(strings[6])));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[7]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[8]));
								sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", new_user_code));

								try
								{
									message = ((int)ServerRequests.AddClient).ToString();
									if (await sqlcommand.ExecuteNonQueryAsync() != 2) message = "0";
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
									message = "error";
									connection.Close();
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.DeleteClient).ToString())
		{
			Console.WriteLine($"Клиент-сотрудник {tcpClient.Client.RemoteEndPoint} запросил удаление клиента");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Delete from users where id = @user_id";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@user_id", Convert.ToInt32(strings[1])));
						try
						{
							message = ((int)ServerRequests.DeleteClient).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.UpdateUser).ToString())
		{
			Console.WriteLine($"Клиент-администратор {tcpClient.Client.RemoteEndPoint} запросил обновление пользователя");
			if (strings[1] == "Клиент")
			{
				using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
				{
					string sql = "update Client set c_name = @name, surname = @surname, middlename = @middlename, " +
						"adres = @adres, phone = @phone, date_b = @birth where id = @client_id;" +
					 "update Passwords set login = @login, p_password = @password " +
						"where user_id = @client_id";

					try
					{
						connection.Open();
						using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
						{
							sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[2]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[3]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[4]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@adres", strings[5]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@phone", strings[6]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@birth", Convert.ToDateTime(strings[7])));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[8]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[9]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", Convert.ToInt32(strings[10])));

							try
							{
								message = ((int)ServerRequests.UpdateUser).ToString();
								if (await sqlcommand.ExecuteNonQueryAsync() != 2) message = "0";
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
								message = "error";
								connection.Close();
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						message = "error";
						if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
					}
				}
			}
			else
			{
				using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
				{
					string sql = "update Employees set c_name = @name, surname = @surname, middlename = @middlename, " +
						"adres = @adres, phone = @phone, date_b = @birth," +
						" e_role = (select id from Roles where role_name = @role), salary = @salary" +
						" where id = @user_id;" +
					 "update Passwords set login = @login, p_password = @password " +
						"where user_id = @user_id";

					try
					{
						connection.Open();
						using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
						{
							sqlcommand.Parameters.Add(new NpgsqlParameter("@role", strings[1]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[2]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[3]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[4]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@adres", strings[5]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@phone", strings[6]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@birth", Convert.ToDateTime(strings[7])));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[8]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[9]));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@user_id", Convert.ToInt32(strings[10])));
							sqlcommand.Parameters.Add(new NpgsqlParameter("@salary", Convert.ToInt32(strings[11])));

							try
							{
								message = ((int)ServerRequests.UpdateUser).ToString();
								if (await sqlcommand.ExecuteNonQueryAsync() != 2) message = "0";
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
								message = "error";
								connection.Close();
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						message = "error";
						if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
					}
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.AddUser).ToString())
		{
			Console.WriteLine($"Клиент-администратор {tcpClient.Client.RemoteEndPoint} запросил добавление клиента");
			if (strings[1] == "Клиент")
			{
				using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
				{
					string sql = "Insert into Users(u_role)" +
						" values ((select id from Roles where role_name = 'Клиент'));";

					try
					{
						connection.Open();

						using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
						{
							try
							{
								message = ((int)ServerRequests.AddUser).ToString();
								if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
								message = "error";
								connection.Close();
							}
						}
						if (message != "error" || message != "0")
						{
							sql = "select Users.id from Users where u_role = (select id from Roles where role_name = 'Клиент') " +
								"and Not exists(select Passwords.id from Passwords where Passwords.id = Users.id);";
							int new_user_code = 0;
							using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
							{
								try
								{
									using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
									{
										if (reader.Read())
										{
											new_user_code = reader.GetInt32(0);
										}
										else
										{
											message = "0";
										}
									}
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
									message = "error";
									connection.Close();
								}
							}
							if (new_user_code != 0)
							{
								sql = "insert into client values " +
									"(@client_id, @name, @surname, @middlename, @adres, @phone, @date_b);" +
								"insert into passwords(login, p_password, user_id) values " +
									"(@login, @password, @client_id);";
								using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
								{
									sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[2]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[3]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[4]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@adres", strings[5]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@phone", strings[6]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@date_b", Convert.ToDateTime(strings[7])));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[8]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[9]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", new_user_code));

									try
									{
										message = ((int)ServerRequests.AddUser).ToString();
										if (await sqlcommand.ExecuteNonQueryAsync() != 2) message = "0";
									}
									catch (Exception ex)
									{
										Console.WriteLine(ex.Message);
										message = "error";
										connection.Close();
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						message = "error";
						if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
					}
				}
			}
			else
			{
				using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
				{
					string sql = "Insert into Users(u_role)" +
						" values ((select id from Roles where role_name = @role));";

					try
					{
						connection.Open();

						using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
						{
							sqlcommand.Parameters.Add(new NpgsqlParameter("@role", strings[1]));
							try
							{
								message = ((int)ServerRequests.AddUser).ToString();
								if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
								message = "error";
								connection.Close();
							}
						}
						if (message != "error" || message != "0")
						{
							sql = "select Users.id from Users where u_role = (select id from Roles where role_name = @role) " +
								"and Not exists(select Passwords.id from Passwords where Passwords.id = Users.id);";
							int new_user_code = 0;
							using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
							{
								sqlcommand.Parameters.Add(new NpgsqlParameter("@role", strings[1]));
								try
								{
									using (NpgsqlDataReader reader = sqlcommand.ExecuteReader())
									{
										if (reader.Read())
										{
											new_user_code = reader.GetInt32(0);
										}
										else
										{
											message = "0";
										}
									}
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
									message = "error";
									connection.Close();
								}
							}
							if (new_user_code != 0)
							{
								sql = "insert into employees values " +
									"(@client_id, (select id from Roles where role_name = @role), @name, @surname, @middlename, @adres, @phone, @date_b, @salary);" +
								"insert into passwords(login, p_password, user_id) values " +
									"(@login, @password, @client_id);";
								using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
								{
									sqlcommand.Parameters.Add(new NpgsqlParameter("@role", strings[1]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@name", strings[2]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@surname", strings[3]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@middlename", strings[4]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@adres", strings[5]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@phone", strings[6]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@date_b", Convert.ToDateTime(strings[7])));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@login", strings[8]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@password", strings[9]));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@salary", Convert.ToInt32(strings[10])));
									sqlcommand.Parameters.Add(new NpgsqlParameter("@client_id", new_user_code));

									try
									{
										message = ((int)ServerRequests.AddUser).ToString();
										if (await sqlcommand.ExecuteNonQueryAsync() != 2) message = "0";
									}
									catch (Exception ex)
									{
										Console.WriteLine(ex.Message);
										message = "error";
										connection.Close();
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						message = "error";
						if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
					}
				}
			}
		}
		else if (strings[0] == ((int)ServerRequests.DeleteUser).ToString())
		{
			Console.WriteLine($"Клиент-администратор {tcpClient.Client.RemoteEndPoint} запросил удаление пользователя");
			using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
			{
				string sql = "Delete from users where id = @user_id";
				try
				{
					connection.Open();
					using (NpgsqlCommand sqlcommand = new NpgsqlCommand(sql, connection))
					{
						sqlcommand.Parameters.Add(new NpgsqlParameter("@user_id", Convert.ToInt32(strings[1])));
						try
						{
							message = ((int)ServerRequests.DeleteUser).ToString();
							if (await sqlcommand.ExecuteNonQueryAsync() != 1) message = "0";
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							message = "error";
							connection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					message = "error";
					if (connection.FullState == System.Data.ConnectionState.Open) connection.Close();
				}
			}
		}

		await stream.WriteAsync(Encoding.UTF8.GetBytes(message + "\n"));
		response.Clear();
		message = "";
	}
	tcpClient.Close();
}