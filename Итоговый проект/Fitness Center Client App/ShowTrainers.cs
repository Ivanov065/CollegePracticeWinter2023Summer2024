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
	public partial class ShowTrainers : Form
	{
		Form parent;
		public ShowTrainers(Form parent)
		{
			InitializeComponent();
			LoadTrainers();
			this.parent = parent;
		}

		async public void LoadTrainers()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			string message = ((int)ServerRequests.LoadTrainers).ToString();

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
					DataTable table = new DataTable();
					table.Columns.Add("ФИО Тренера".ToString());
					table.Columns.Add("Дата Рождения".ToString());
					table.Columns.Add("Адрес".ToString());
					table.Columns.Add("Телефон".ToString());
					table.Columns.Add("Зарплата".ToString());

					for (int i = 0; i < counter; i++)
					{
						DataRow dr = table.NewRow();
						dr["ФИО Тренера"] = strings[2 + 7 * i] + " " + strings[3 + 7 * i] + " " + strings[4 + 7 * i];
						dr["Дата Рождения"] = strings[5 + 7 * i];
						dr["Адрес"] = strings[6 + 7 * i];
						dr["Телефон"] = strings[7 + 7 * i];
						dr["Зарплата"] = strings[8 + 7 * i];
						table.Rows.Add(dr);
					}
					dataGridViewTrainers.DataSource = null;
					dataGridViewTrainers.DataSource = table;
				}
			}
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
