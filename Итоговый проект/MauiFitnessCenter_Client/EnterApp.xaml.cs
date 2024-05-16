using System.Data;
using System.Net.Sockets;
using System.Text;

namespace MauiFitnessCenter_Client
{
	public partial class EnterApp : ContentPage
	{
		public EnterApp()
		{
			InitializeComponent();
		}

		async private void ButtonLogin_Click(object sender, EventArgs e)
		{
			if (entryLogin.Text == "" || entryPassword.Text == "")
			{
				await DisplayAlert("Внимание", "Ввод пустых значений", "ОК");
				return;
			}

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoginUser) + "|";
			message += entryLogin.Text + "|";
			message += entryPassword.Text;
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

			if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого пользователя не существует", "ОК");
			else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
			else
			{
				string[] strings = translation.Split('|');
				User user = new User(Convert.ToInt32(strings[1]), strings[2]);
				if (user.Role == ((int)Roles.Client).ToString())
				{
					await Navigation.PushAsync(new ClientMenu(user));
				}
				else if (user.Role == ((int)Roles.Trainer).ToString())
				{
					await Navigation.PushAsync(new TrainerMenu(user));
				}
				else if (user.Role == ((int)Roles.Manager).ToString() || user.Role == ((int)Roles.Administrator).ToString())
				{
					await Navigation.PushAsync(new ManagerMenu(user));
				}
			}
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			Application.Current.Quit();
		}
	}
}