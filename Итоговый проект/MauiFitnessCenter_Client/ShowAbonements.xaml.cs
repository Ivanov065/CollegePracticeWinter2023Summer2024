using System.Data;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiFitnessCenter_Client;

public partial class ShowAbonements : ContentPage
{
	public ShowAbonements()
	{
		InitializeComponent();
		InitAndFillTickets();
	}

	private async void ButtonExit_Click(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого пользователя не существует", "ОК");
		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			string[] strings = translation.Split('|');
			int counter = Convert.ToInt32(strings[1]);
			dataGridTickets.ItemsSource = null;
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
				dataGridTickets.ItemsSource = table;
			}
		}
	}
}