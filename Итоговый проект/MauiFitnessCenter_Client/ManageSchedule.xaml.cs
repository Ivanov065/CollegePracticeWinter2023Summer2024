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

namespace MauiFitnessCenter_Client;

public partial class ManageSchedule : ContentPage
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

	User user;
	Mode mode = Mode.Add;
	List<Trainer> trainers = new List<Trainer>();

	public ManageSchedule(User user)
	{
		InitializeComponent();
		this.user = user;

		labelScheduleCode.IsEnabled = false;
		labelScheduleCode.IsVisible = false;
		entryScheduleCode.IsEnabled = false;
		entryScheduleCode.IsVisible = false;
		buttonFindSchedule.IsEnabled = false;
		buttonFindSchedule.IsVisible = false;
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Ничего не найдено", "ОК");
		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			string[] strings = translation.Split('|');
			int counter_training = Convert.ToInt32(strings[1]),
				counter_serv = Convert.ToInt32(strings[2]),
				counter_trainers = Convert.ToInt32(strings[3]);

			if (counter_training == 0 || counter_serv == 0 || counter_trainers == 0)
			{
				await DisplayAlert("Внимание", "Ошибка: Пустые таблицы в базе данных. Запросите помощи у Администратора", "ОК");
				await Navigation.PopAsync();
			}
			else
			{
				List<string> trainingTypesItems = new List<string>();
				List<string> serviceTypesItems = new List<string>();
				List<string> trainersItems = new List<string>();

				for (int i = 0; i < counter_training; i++)
				{
					trainingTypesItems.Add(strings[4 + i]);
					//pickerTrainingTypes.Items.Add(strings[4 + i]);
				}
				for (int i = 0; i < counter_serv; i++)
				{
					serviceTypesItems.Add(strings[4 + counter_training + i]);
					//pickerServiceTypes.Items.Add(strings[4 + counter_training + i]);
				}
				trainers.Clear();
				for (int i = 0; i < counter_trainers; i++)
				{
					trainers.Add(new Trainer(strings[4 + counter_training + counter_serv + 3 * i],
						strings[5 + counter_training + counter_serv + 3 * i],
						strings[6 + counter_training + counter_serv + 3 * i]));

					trainersItems.Add(trainers[i].ToString());
				}
				pickerTrainingTypes.ItemsSource = trainingTypesItems;
				pickerServiceTypes.ItemsSource = serviceTypesItems;
				pickerTrainers.ItemsSource = trainersItems;
			}
		}
	}

	private void AddMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Add)
		{
			mode = Mode.Add;

			labelTitle.Text = "Режим Добавления Расписания";

			labelScheduleCode.IsEnabled = false;
			labelScheduleCode.IsVisible = false;
			entryScheduleCode.IsEnabled = false;
			entryScheduleCode.IsVisible = false;
			entryScheduleCode.Text = string.Empty;
			buttonFindSchedule.IsEnabled = false;
			buttonFindSchedule.IsVisible = false;

			datePickerDateConduction.Date = DateTime.Now;
			datePickerDateConduction.IsEnabled = true;

			timePickerTimeStart.Time = TimeSpan.Zero;
			timePickerTimeStart.IsEnabled = true;

			timePickerTimeEnd.Time = TimeSpan.Zero;
			timePickerTimeEnd.IsEnabled = true;

			pickerTrainers.Title = string.Empty;
			pickerTrainers.IsEnabled = true;

			pickerServiceTypes.Title = string.Empty;
			pickerServiceTypes.IsEnabled = true;

			pickerTrainingTypes.Title = string.Empty;
			pickerTrainingTypes.IsEnabled = true;

			buttonEdit.IsEnabled = true;
		}
	}

	private void UpdateMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Update)
		{
			mode = Mode.Update;
			labelTitle.Text = "Режим Обновления Расписания";

			labelScheduleCode.IsEnabled = true;
			labelScheduleCode.IsVisible = true;
			entryScheduleCode.IsEnabled = true;
			entryScheduleCode.IsVisible = true;
			entryScheduleCode.Text = string.Empty;
			buttonFindSchedule.IsEnabled = true;
			buttonFindSchedule.IsVisible = true;

			datePickerDateConduction.Date = DateTime.Now;
			datePickerDateConduction.IsEnabled = true;

			timePickerTimeStart.Time = TimeSpan.Zero;
			timePickerTimeStart.IsEnabled = true;

			timePickerTimeEnd.Time = TimeSpan.Zero;
			timePickerTimeEnd.IsEnabled = true;

			pickerTrainers.Title = string.Empty;
			pickerTrainers.IsEnabled = true;

			pickerServiceTypes.Title = string.Empty;
			pickerServiceTypes.IsEnabled = true;

			pickerTrainingTypes.Title = string.Empty;
			pickerTrainingTypes.IsEnabled = true;

			buttonEdit.IsEnabled = false;
		}
	}

	private void DeleteMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Delete)
		{
			mode = Mode.Delete;
			labelTitle.Text = "Режим Удаления Расписания";

			labelScheduleCode.IsEnabled = true;
			labelScheduleCode.IsVisible = true;
			entryScheduleCode.IsEnabled = true;
			entryScheduleCode.IsVisible = true;
			entryScheduleCode.Text = string.Empty;
			buttonFindSchedule.IsEnabled = true;
			buttonFindSchedule.IsVisible = true;

			datePickerDateConduction.Date = DateTime.Now;
			datePickerDateConduction.IsEnabled = false;

			timePickerTimeStart.Time = TimeSpan.Zero;
			timePickerTimeStart.IsEnabled = false;

			timePickerTimeEnd.Time = TimeSpan.Zero;
			timePickerTimeEnd.IsEnabled = false;

			pickerTrainers.Title = string.Empty;
			pickerTrainers.IsEnabled = false;

			pickerServiceTypes.Title = string.Empty;
			pickerServiceTypes.IsEnabled = false;

			pickerTrainingTypes.Title = string.Empty;
			pickerTrainingTypes.IsEnabled = false;

			buttonEdit.IsEnabled = false;
		}
	}

	private async void ButtonExit_Click(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
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
		if (entryScheduleCode.Text != string.Empty) LoadSingleScheduleInfo();
	}

	async public void LoadSingleScheduleInfo()
	{
		//Подготавливаем структуру для сообщения
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// Само сообщение
		string message = ((int)ServerRequests.LoadSingleSchedule).ToString()
			+ "|" + entryScheduleCode.Text;
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого расписания не существует", "ОК"); 		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			string[] strings = translation.Split('|');
			datePickerDateConduction.Date = DateTime.Parse(strings[1]);
			timePickerTimeStart.Time = DateTime.Parse(strings[2]).TimeOfDay; 
			timePickerTimeEnd.Time = DateTime.Parse(strings[3]).TimeOfDay;
			//pickerTrainers.Title = strings[5] + " " + strings[4][0] + ". " + strings[6][0] + ".";
			for (int i = 0; i < pickerTrainers.Items.Count; i++)
			{
				if (pickerTrainers.Items[i] == strings[5] + " " + strings[4][0] + ". " + strings[6][0] + ".") pickerTrainers.SelectedIndex = i;
			}
			for (int i = 0; i < pickerTrainingTypes.Items.Count; i++)
			{
				if (pickerTrainingTypes.Items[i] == strings[8]) pickerTrainingTypes.SelectedIndex = i;
			}
			for (int i = 0; i < pickerServiceTypes.Items.Count; i++)
			{
				if (pickerServiceTypes.Items[i] == strings[7]) pickerServiceTypes.SelectedIndex = i;
			}

			buttonEdit.IsEnabled = true;
		}
	}

	async public void UpdateSchedule()
	{
		if (pickerServiceTypes.SelectedItem.ToString() == "" ||
			pickerTrainingTypes.SelectedItem.ToString() == "" ||
			pickerTrainers.SelectedItem.ToString() == "")
		{
			await DisplayAlert("Внимание", "Ввод пустых значений недопустим", "ОК");
			return;
		}
		if (DateCorrectionChecker())
		{
			await DisplayAlert("Внимание", "Неправильный ввод временных диапазонов", "ОК");
			return;
		}
		//Подготавливаем структуру для сообщения
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// Само сообщение
		string message = ((int)ServerRequests.UpdateSchedule).ToString() + "|" +
			datePickerDateConduction.Date.ToString() + "|" + timePickerTimeStart.Time.ToString() + "|" +
			timePickerTimeEnd.Time.ToString() + "|" + trainers[pickerTrainers.SelectedIndex].Name + "|" +
			trainers[pickerTrainers.SelectedIndex].Surname + "|" + trainers[pickerTrainers.SelectedIndex].Middlename + "|" +
			pickerServiceTypes.SelectedItem.ToString() + "|" + pickerTrainingTypes.SelectedItem.ToString() + "|" +
			entryScheduleCode.Text.Replace("|", "]");
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого расписания не существует", "ОК");
		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			await DisplayAlert("Успех", "Расписание успешно обновлено", "ОК");
		}
	}

	async public void DeleteSchedule()
	{
		if (pickerServiceTypes.SelectedItem.ToString() == "" ||
			pickerTrainingTypes.SelectedItem.ToString() == "" ||
			pickerTrainers.SelectedItem.ToString() == "")
		{
			await DisplayAlert("Внимание", "Ввод пустых значений недопустим", "ОК");
			return;
		}

		//Подготавливаем структуру для сообщения
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// Само сообщение
		string message = ((int)ServerRequests.DeleteSchedule).ToString()
			+ "|" + entryScheduleCode.Text;
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого расписания не существует", "ОК");
		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			await DisplayAlert("Успех", "Расписание успешно удалено", "ОК");
		}
	}

	async public void AddSchedule()
	{
		if (pickerServiceTypes.SelectedItem.ToString() == "" ||
			pickerTrainingTypes.SelectedItem.ToString() == "" ||
			pickerTrainers.SelectedItem.ToString() == "")
		{
			await DisplayAlert("Внимание", "Ввод пустых значений недопустим", "ОК");
			return;
		}
		if (DateCorrectionChecker())
		{
			await DisplayAlert("Внимание", "Неправильный ввод временных диапазонов", "ОК");
			return;
		}

		//Подготавливаем структуру для сообщения
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// Само сообщение
		string message = ((int)ServerRequests.AddSchedule).ToString() + "|" +
			datePickerDateConduction.Date.ToString() + "|" + timePickerTimeStart.Time.ToString() + "|" +
			timePickerTimeEnd.Time.ToString() + "|" + trainers[pickerTrainers.SelectedIndex].Name + "|" +
			trainers[pickerTrainers.SelectedIndex].Surname + "|" + trainers[pickerTrainers.SelectedIndex].Middlename + "|" +
			pickerServiceTypes.SelectedItem.ToString() + "|" + pickerTrainingTypes.SelectedItem.ToString() + "|" +
			entryScheduleCode.Text.Replace("|", "]");
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

		if (translation == "0") await DisplayAlert("Внимание", "Ошибка: Такого расписания не существует", "ОК");
		else if (translation == "error") await DisplayAlert("Внимание", "Ошибка: Проблема с обработкой запроса из базы данных", "ОК");
		else
		{
			await DisplayAlert("Успех", "Расписание успешно добавлено", "ОК");
		}
	}

	public bool DateCorrectionChecker()
	{
		TimeSpan start = timePickerTimeStart.Time;
		TimeSpan end = timePickerTimeEnd.Time;
		return TimeSpan.Compare(start, end) >= 0 ||
			DateTime.Compare(datePickerDateConduction.Date, DateTime.Now) < 0;
	}

	private void TextBoxScheduleCode_TextChanged(object sender, EventArgs e)
	{
		buttonEdit.IsEnabled = false;
	}
}