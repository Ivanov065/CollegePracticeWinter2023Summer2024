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
	public partial class EmployeeAcc : Form
	{
		Form parent;
		User user;
		public EmployeeAcc(Form parent, User user)
		{
			InitializeComponent();
			this.parent = parent;
			this.user = user;
			LoadUser(user);
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		async public void LoadUser(User user)
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadEmployee) + "|";
			message += user.ID + "|" + user.Role;
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
			
				labelFIO.Text += strings[1] + " " + strings[2] + " " + strings[3];
				labelAdres.Text += strings[4];
				labelPhone.Text += strings[5];
				labelBirthday.Text += strings[6];
				labelRole.Text += strings[7];
				labelSalary.Text += strings[8];
			}
		}
	}
}
