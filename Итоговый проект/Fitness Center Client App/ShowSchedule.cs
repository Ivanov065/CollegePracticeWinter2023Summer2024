using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness_Center_Client_App
{
	public partial class ShowSchedule : Form
	{
		Form parent;
		User user;
		public ShowSchedule(Form parent, User user)
		{
			InitializeComponent();
			this.parent = parent;
			this.user = user;
			dateTimePickerScheduleDate.Value = DateTime.Now;
			if (user.Role != ((int)Roles.Trainer).ToString())
			{
				buttonTrainerSchedule.Enabled = false;
				buttonTrainerSchedule.Visible = false;
			}
			LoadSchedule(false);
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonRefreshTable_Click(object sender, EventArgs e)
		{
			LoadSchedule(false);
		}

		async public void LoadSchedule(bool is_trainer)
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			string message;
			if (!is_trainer) message = ((int)ServerRequests.LoadSchedule).ToString() + "|" + dateTimePickerScheduleDate.Value.Date.ToString();
			else message = ((int)ServerRequests.LoadTrainerSchedule).ToString() + "|" + dateTimePickerScheduleDate.Value.Date.ToString()
					+ "|" + user.ID;

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
				if (strings.Length > 1)
				{
					int counter = Convert.ToInt32(strings[1]);
					dataGridViewSchedule.DataSource = null;
					if (counter > 0)
					{
						DataTable table = new DataTable();
						table.Columns.Add("Код записи".ToString());
						table.Columns.Add("Время Начала".ToString());
						table.Columns.Add("Время Окончания".ToString());
						table.Columns.Add("Тренер".ToString());
						table.Columns.Add("Тип Услуги".ToString());
						table.Columns.Add("Тип Тренировки".ToString());

						for (int i = 0; i < counter; i++)
						{
							DataRow dr = table.NewRow();
							dr["Код записи"] = strings[2 + 7 * i];
							dr["Время Начала"] = strings[3 + 7 * i];
							dr["Время Окончания"] = strings[4 + 7 * i];
							dr["Тренер"] = strings[5 + 7 * i] + " " + strings[6 + 7 * i];
							dr["Тип Услуги"] = strings[7 + 7 * i];
							dr["Тип Тренировки"] = strings[8 + 7 * i];
							table.Rows.Add(dr);
						}
						dataGridViewSchedule.DataSource = table;
					}
				}
			}
		}

		private void ButtonTrainerSchedule_Click(object sender, EventArgs e)
		{
			LoadSchedule(true);
		}
	}
}
