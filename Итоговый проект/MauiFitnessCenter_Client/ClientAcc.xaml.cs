using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiFitnessCenter_Client;

public partial class ClientAcc : ContentPage
{
	User user;
	public ClientAcc(User user)
	{
		InitializeComponent();
		this.user = user;
		LoadUser(this.user);
	}

	private async void ButtonExit_Click(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	async public void LoadUser(User user)
	{
		//Подготавливаем структуру для сообщения
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// Само сообщение
		string message = ((int)ServerRequests.LoadClient) + "|";
		message += user.ID;
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
			if (strings[1] == "1")
			{
				labelFIO.Text += strings[2] + " " + strings[3] + " " + strings[4];
				labelAdres.Text += strings[5];
				labelPhone.Text += strings[6];
				labelBirthday.Text += strings[7];
				labelSeasonTicketId.Text += strings[8];
				labelTimeActive.Text += strings[9] + " - " + strings[10];
				labelRemainVisits.Text += strings[11];
				labelTrainingType.Text += strings[12];
				labelServiceType.Text += strings[13];
				labelTicketPrice.Text += strings[14];
				labelAmountVisits.Text += strings[15];
			}
			else
			{
				labelFIO.Text += strings[2] + " " + strings[3] + " " + strings[4];
				labelAdres.Text += strings[5];
				labelPhone.Text += strings[6];
				labelBirthday.Text += strings[7];
				labelSeasonTicketId.Text = "У вас сейчас нет привязанного абонемента";
			}
		}
	}
}