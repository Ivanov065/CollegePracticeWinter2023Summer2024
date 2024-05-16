using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness_Center_Client_App
{
	public partial class ManageSchedule : Form
	{
		private enum Mode
		{
			Update, Delete, Add
		}

		private class Trainer
		{
			public string Name { get; set; }
			public string Surname { get; set; }
			public string Middlename { get; set; }

			public Trainer(string name, string surname, string middlename)
			{
				Name = name;
				Surname = surname;
				Middlename = middlename;
			}

			public override string ToString()
			{
				return Surname + " " + Name[0] + ". " + Middlename[0] + ".";
			}
		}

		Form parent;
		User user;
		Mode mode = Mode.Add;
		List<Trainer> trainers = new List<Trainer>();

		public ManageSchedule(Form parent, User user)
		{
			InitializeComponent();
			this.parent = parent;
			this.user = user;

			labelScheduleCode.Enabled = false;
			labelScheduleCode.Visible = false;
			textBoxScheduleCode.Enabled = false;
			textBoxScheduleCode.Visible = false;
			buttonFindSchedule.Enabled = false;
			buttonFindSchedule.Visible = false;
			LoadManageScheduleInfo();
		}

		async public void LoadManageScheduleInfo()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadManageScheduleInfo).ToString();
			// Ответное сообщение
			var response = new List<byte>();
			int bytesRead = 10; // для считывания байтов из потока TMP'шка

			// считываем строку в массив байтов
			// при отправке добавляем маркер завершения сообщения - \n
			byte[] data = Encoding.UTF8.GetBytes(message + '\n');
			// отправляем данные
			await stream.WriteAsync(data);
			// считываем данные до конечного символа
			while ((bytesRead = stream.ReadByte()) != '\n')
			{
				// добавляем в буфер
				response.Add((byte)bytesRead);
			}
			var translation = Encoding.UTF8.GetString(response.ToArray());
			await stream.WriteAsync(Encoding.UTF8.GetBytes(((int)ServerRequests.EndConnection).ToString() + "\n"));

			if (translation == "0") MessageBox.Show("Error: User not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				string[] strings = translation.Split('|');
				int counter_training = Convert.ToInt32(strings[1]),
					counter_serv = Convert.ToInt32(strings[2]),
					counter_trainers = Convert.ToInt32(strings[3]);

				if (counter_training == 0 || counter_serv == 0 || counter_trainers == 0)
				{
					MessageBox.Show("Error: Empty tables in DB. Ask administrator for help.");
					this.Close();
				}
				else
				{
					for (int i = 0; i < counter_training; i++)
					{
						comboBoxTrainingTypes.Items.Add(strings[4 + i]);
					}
					for (int i = 0; i < counter_serv; i++)
					{
						comboBoxServiceTypes.Items.Add(strings[4 + counter_training + i]);
					}
					trainers.Clear();
					for (int i = 0; i < counter_trainers; i++)
					{
						trainers.Add(new Trainer(strings[4 + counter_training + counter_serv + 3 * i],
							strings[5 + counter_training + counter_serv + 3 * i],
							strings[6 + counter_training + counter_serv + 3 * i]));

						comboBoxTrainers.Items.Add(trainers[i].ToString());
					}
				}
			}
		}

		private void AddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Add)
			{
				mode = Mode.Add;

				labelTitle.Text = "Режим Добавления Расписания";

				labelScheduleCode.Enabled = false;
				labelScheduleCode.Visible = false;
				textBoxScheduleCode.Enabled = false;
				textBoxScheduleCode.Visible = false;
				textBoxScheduleCode.Text = string.Empty;
				buttonFindSchedule.Enabled = false;
				buttonFindSchedule.Visible = false;

				dateTimePickerDateConduction.Text = string.Empty;
				dateTimePickerDateConduction.Enabled = true;

				dateTimePickerTimeStart.Text = string.Empty;
				dateTimePickerTimeStart.Enabled = true;

				dateTimePickerTimeEnd.Text = string.Empty;
				dateTimePickerTimeEnd.Enabled = true;

				comboBoxTrainers.Text = string.Empty;
				comboBoxTrainers.Enabled = true;

				comboBoxServiceTypes.Text = string.Empty;
				comboBoxServiceTypes.Enabled = true;

				comboBoxTrainingTypes.Text = string.Empty;
				comboBoxTrainingTypes.Enabled = true;

				buttonEdit.Enabled = true;
			}
		}

		private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Update)
			{
				mode = Mode.Update;
				labelTitle.Text = "Режим Обновления Расписания";

				labelScheduleCode.Enabled = true;
				labelScheduleCode.Visible = true;
				textBoxScheduleCode.Enabled = true;
				textBoxScheduleCode.Visible = true;
				textBoxScheduleCode.Text = string.Empty;
				buttonFindSchedule.Enabled = true;
				buttonFindSchedule.Visible = true;

				dateTimePickerDateConduction.Text = string.Empty;
				dateTimePickerDateConduction.Enabled = true;

				dateTimePickerTimeStart.Text = string.Empty;
				dateTimePickerTimeStart.Enabled = true;

				dateTimePickerTimeEnd.Text = string.Empty;
				dateTimePickerTimeEnd.Enabled = true;

				comboBoxTrainers.Text = string.Empty;
				comboBoxTrainers.Enabled = true;

				comboBoxServiceTypes.Text = string.Empty;
				comboBoxServiceTypes.Enabled = true;

				comboBoxTrainingTypes.Text = string.Empty;
				comboBoxTrainingTypes.Enabled = true;

				buttonEdit.Enabled = false;
			}
		}

		private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Delete)
			{
				mode = Mode.Delete;
				labelTitle.Text = "Режим Удаления Расписания";

				labelScheduleCode.Enabled = true;
				labelScheduleCode.Visible = true;
				textBoxScheduleCode.Enabled = true;
				textBoxScheduleCode.Visible = true;
				textBoxScheduleCode.Text = string.Empty;
				buttonFindSchedule.Enabled = true;
				buttonFindSchedule.Visible = true;

				dateTimePickerDateConduction.Text = string.Empty;
				dateTimePickerDateConduction.Enabled = false;

				dateTimePickerTimeStart.Text = string.Empty;
				dateTimePickerTimeStart.Enabled = false;

				dateTimePickerTimeEnd.Text = string.Empty;
				dateTimePickerTimeEnd.Enabled = false;

				comboBoxTrainers.Text = string.Empty;
				comboBoxTrainers.Enabled = false;

				comboBoxServiceTypes.Text = string.Empty;
				comboBoxServiceTypes.Enabled = false;

				comboBoxTrainingTypes.Text = string.Empty;
				comboBoxTrainingTypes.Enabled = false;

				buttonEdit.Enabled = false;
			}
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonEdit_Click(object sender, EventArgs e)
		{
			switch (mode)
			{
				case Mode.Add:
					AddSchedule();
					break;
				case Mode.Delete:
					DeleteSchedule();
					break;
				case Mode.Update:
					UpdateSchedule();
					break;
				default:
					break;

			}
		}

		private void ButtonFindSchedule_Click(object sender, EventArgs e)
		{
			if (textBoxScheduleCode.Text != string.Empty) LoadSingleScheduleInfo();
		}

		async public void LoadSingleScheduleInfo()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadSingleSchedule).ToString()
				+ "|" + textBoxScheduleCode.Text;
			// Ответное сообщение
			var response = new List<byte>();
			int bytesRead = 10; // для считывания байтов из потока TMP'шка

			// считываем строку в массив байтов
			// при отправке добавляем маркер завершения сообщения - \n
			byte[] data = Encoding.UTF8.GetBytes(message + '\n');
			// отправляем данные
			await stream.WriteAsync(data);
			// считываем данные до конечного символа
			while ((bytesRead = stream.ReadByte()) != '\n')
			{
				// добавляем в буфер
				response.Add((byte)bytesRead);
			}
			var translation = Encoding.UTF8.GetString(response.ToArray());
			await stream.WriteAsync(Encoding.UTF8.GetBytes(((int)ServerRequests.EndConnection).ToString() + "\n"));

			if (translation == "0") MessageBox.Show("Error: Schedule not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				string[] strings = translation.Split('|');
				dateTimePickerDateConduction.Text = strings[1];
				dateTimePickerTimeStart.Text = strings[2];
				dateTimePickerTimeEnd.Text = strings[3];
				comboBoxTrainers.Text = strings[5] + " " + strings[4][0] + ". " + strings[6][0] + ".";
				comboBoxServiceTypes.Text = strings[7];
				comboBoxTrainingTypes.Text = strings[8];

				buttonEdit.Enabled = true;
			}
		}

		async public void UpdateSchedule()
		{
			if (comboBoxServiceTypes.Text == "" || comboBoxTrainingTypes.Text == "" || comboBoxTrainers.Text == "")
			{
				MessageBox.Show("Пустой ввод невозможен");
				return;
			}
			if (DateCorrectionChecker())
			{
				MessageBox.Show("Неправильный ввод временных диапазонов");
				return;
			}
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.UpdateSchedule).ToString() + "|" +
				dateTimePickerDateConduction.Text + "|" + dateTimePickerTimeStart.Text + "|" +
				dateTimePickerTimeEnd.Text + "|" + trainers[comboBoxTrainers.SelectedIndex].Name + "|" +
				trainers[comboBoxTrainers.SelectedIndex].Surname + "|" + trainers[comboBoxTrainers.SelectedIndex].Middlename + "|" +
				comboBoxServiceTypes.Text + "|" + comboBoxTrainingTypes.Text + "|" +
				textBoxScheduleCode.Text.Replace("|", "]");
			// Ответное сообщение
			var response = new List<byte>();
			int bytesRead = 10; // для считывания байтов из потока TMP'шка

			// считываем строку в массив байтов
			// при отправке добавляем маркер завершения сообщения - \n
			byte[] data = Encoding.UTF8.GetBytes(message + '\n');
			// отправляем данные
			await stream.WriteAsync(data);
			// считываем данные до конечного символа
			while ((bytesRead = stream.ReadByte()) != '\n')
			{
				// добавляем в буфер
				response.Add((byte)bytesRead);
			}
			var translation = Encoding.UTF8.GetString(response.ToArray());
			await stream.WriteAsync(Encoding.UTF8.GetBytes(((int)ServerRequests.EndConnection).ToString() + "\n"));

			if (translation == "0") MessageBox.Show("Error: Schedule not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Schedule Updated successfully");
			}
		}

		async public void DeleteSchedule()
		{
			if (comboBoxServiceTypes.Text == "" || comboBoxTrainingTypes.Text == "" || comboBoxTrainers.Text == "")
			{
				MessageBox.Show("Пустой ввод невозможен");
				return;
			}

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.DeleteSchedule).ToString()
				+ "|" + textBoxScheduleCode.Text;
			// Ответное сообщение
			var response = new List<byte>();
			int bytesRead = 10; // для считывания байтов из потока TMP'шка

			// считываем строку в массив байтов
			// при отправке добавляем маркер завершения сообщения - \n
			byte[] data = Encoding.UTF8.GetBytes(message + '\n');
			// отправляем данные
			await stream.WriteAsync(data);
			// считываем данные до конечного символа
			while ((bytesRead = stream.ReadByte()) != '\n')
			{
				// добавляем в буфер
				response.Add((byte)bytesRead);
			}
			var translation = Encoding.UTF8.GetString(response.ToArray());
			await stream.WriteAsync(Encoding.UTF8.GetBytes(((int)ServerRequests.EndConnection).ToString() + "\n"));

			if (translation == "0") MessageBox.Show("Error: Schedule not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Schedule Deleted Successfully");
			}
		}

		async public void AddSchedule()
		{
			if(comboBoxServiceTypes.Text == "" || comboBoxTrainingTypes.Text == "" || comboBoxTrainers.Text == "")
			{
				MessageBox.Show("Пустой ввод невозможен");
				return;
			}
			if (DateCorrectionChecker())
			{
				MessageBox.Show("Неправильный ввод временных диапазонов");
				return;
			}

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.AddSchedule).ToString() + "|" +
				dateTimePickerDateConduction.Text + "|" + dateTimePickerTimeStart.Text + "|" +
				dateTimePickerTimeEnd.Text + "|" + trainers[comboBoxTrainers.SelectedIndex].Name + "|" +
				trainers[comboBoxTrainers.SelectedIndex].Surname + "|" + trainers[comboBoxTrainers.SelectedIndex].Middlename + "|" +
				comboBoxServiceTypes.Text + "|" + comboBoxTrainingTypes.Text;
			// Ответное сообщение
			var response = new List<byte>();
			int bytesRead = 10; // для считывания байтов из потока TMP'шка

			// считываем строку в массив байтов
			// при отправке добавляем маркер завершения сообщения - \n
			byte[] data = Encoding.UTF8.GetBytes(message + '\n');
			// отправляем данные
			await stream.WriteAsync(data);
			// считываем данные до конечного символа
			while ((bytesRead = stream.ReadByte()) != '\n')
			{
				// добавляем в буфер
				response.Add((byte)bytesRead);
			}
			var translation = Encoding.UTF8.GetString(response.ToArray());
			await stream.WriteAsync(Encoding.UTF8.GetBytes(((int)ServerRequests.EndConnection).ToString() + "\n"));

			if (translation == "0") MessageBox.Show("Error: User not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Schedule Add Successfully");
			}
		}

		public bool DateCorrectionChecker()
		{
			TimeSpan start = new TimeSpan(dateTimePickerTimeStart.Value.Hour,
											  dateTimePickerTimeStart.Value.Minute,
											  dateTimePickerTimeStart.Value.Second);
			TimeSpan end = new TimeSpan(dateTimePickerTimeEnd.Value.Hour,
											  dateTimePickerTimeEnd.Value.Minute,
											  dateTimePickerTimeEnd.Value.Second);
			return TimeSpan.Compare(start, end) >= 0 ||
				DateTime.Compare(dateTimePickerDateConduction.Value, DateTime.Now) < 0;
		}

		private void TextBoxScheduleCode_TextChanged(object sender, EventArgs e)
		{
			buttonEdit.Enabled = false;
		}
	}
}
