using System.Net.Sockets;
using System.Text;

namespace Fitness_Center_Client_App
{
	public partial class EnterApp : Form
	{
		ClientMenu clientMenu = null;
		TrainerMenu trainerMenu = null;
		ManagerMenu managerMenu = null;
		
		public EnterApp()
		{
			InitializeComponent();
			buttonLogin.Enabled = false;
		}

		async private void ButtonLogin_Click(object sender, EventArgs e)
		{
			if (textBoxLogin.Text == "" || textBoxPassword.Text == "")
			{
				MessageBox.Show("Empty input error.");
				return;
			}

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoginUser) + "|";
			message += textBoxLogin.Text + "|";
			message += textBoxPassword.Text;
			message.Replace('|', '[');
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
				User user = new User(Convert.ToInt32(strings[1]), strings[2]);
				if (user.Role == ((int)Roles.Client).ToString())
				{
					if (clientMenu == null) clientMenu = new ClientMenu(this, user);
					clientMenu.FormClosed += ClientMenu_FormClosed;
					clientMenu.Show();
					Hide();
				}
				else if (user.Role == ((int)Roles.Trainer).ToString())
				{
					if (trainerMenu == null) trainerMenu = new TrainerMenu(this, user);
					trainerMenu.FormClosed += TrainerMenu_FormClosed;
					trainerMenu.Show();
					Hide();
				}
				else if (user.Role == ((int)Roles.Manager).ToString())
				{
					if (managerMenu == null) managerMenu = new ManagerMenu(this, user);
					managerMenu.FormClosed += ManagerMenu_FormClosed;
					managerMenu.Show();
					Hide();
				}
				else if (user.Role == ((int)Roles.Administrator).ToString())
				{
					if (managerMenu == null) managerMenu = new ManagerMenu(this, user);
					managerMenu.FormClosed += ManagerMenu_FormClosed;
					managerMenu.Show();
					Hide();
				}
			}
		}

		private void ManagerMenu_FormClosed(object sender, EventArgs e)
		{
			managerMenu = null;
			this.Close();
		}

		private void ClientMenu_FormClosed(object sender, EventArgs e)
		{
			clientMenu = null;
			this.Close();
		}

		private void TrainerMenu_FormClosed(object sender, EventArgs e)
		{
			trainerMenu = null;
			this.Close();
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LoginTextBoxes_TextChanged(object sender, EventArgs e)
		{
			if (textBoxLogin.Text != "" && textBoxPassword.Text != "") buttonLogin.Enabled = true;
			else buttonLogin.Enabled = false;
		}
	}
}