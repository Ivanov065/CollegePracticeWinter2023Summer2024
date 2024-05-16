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

public partial class ShowSchedule : ContentPage
{
	User user;

	public ShowSchedule(User user)
	{
		InitializeComponent();
		this.user = user;
		datePickerScheduleDate.Date = DateTime.Now;
		if (user.Role != ((int)Roles.Trainer).ToString())
		{
			buttonTrainerSchedule.IsEnabled = false;
			buttonTrainerSchedule.IsVisible = false;
		}
		LoadSchedule(false);
	}

	private async void ButtonExit_Click(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
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
		if (!is_trainer) message = ((int)ServerRequests.LoadSchedule).ToString() + "|" + datePickerScheduleDate.Date.ToString();
		else message = ((int)ServerRequests.LoadTrainerSchedule).ToString() + "|" + datePickerScheduleDate.Date.ToString()
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого пользователя не существует", "ОК");
		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			string[] strings = translation.Split('|');
			if (strings.Length > 1)
			{
				int counter = Convert.ToInt32(strings[1]);
				dataGridSchedule.ItemsSource = null;
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
					dataGridSchedule.ItemsSource = table;
				}
			}
		}
	}

	private void ButtonTrainerSchedule_Click(object sender, EventArgs e)
	{
		LoadSchedule(true);
	}
}