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
	public partial class ShowAbonements : Form
	{
		Form parent;
		public ShowAbonements(Form parent)
		{
			InitializeComponent();
			InitAndFillTickets();
			this.parent = parent;
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		async public void InitAndFillTickets()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadTickets).ToString();

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
				int counter = Convert.ToInt32(strings[1]);
				dataGridViewTickets.DataSource = null;
				if (counter > 0)
				{
					DataTable table = new DataTable();
					table.Columns.Add("Код Абонемента".ToString());
					table.Columns.Add("Тип Тренировки".ToString());
					table.Columns.Add("Тип Услуги".ToString());
					table.Columns.Add("Цена".ToString());
					table.Columns.Add("Кол-во Посещений".ToString());
					table.Columns.Add("Активен (дней)".ToString());

					for (int i = 0; i < counter; i++)
					{
						DataRow dr = table.NewRow();
						dr["Код Абонемента"] = strings[2 + 6 * i];
						dr["Тип Тренировки"] = strings[3 + 6 * i];
						dr["Тип Услуги"] = strings[4 + 6 * i];
						dr["Цена"] = strings[5 + 6 * i];
						dr["Кол-во Посещений"] = strings[6 + 6 * i];
						dr["Активен (дней)"] = strings[7 + 6 * i];
						table.Rows.Add(dr);
					}
					dataGridViewTickets.DataSource = table;
				}
			}
		}
	}
}
