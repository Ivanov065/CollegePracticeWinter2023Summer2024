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
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		string message;
		if (!is_trainer) message = ((int)ServerRequests.LoadSchedule).ToString() + "|" + datePickerScheduleDate.Date.ToString();
		else message = ((int)ServerRequests.LoadTrainerSchedule).ToString() + "|" + datePickerScheduleDate.Date.ToString()
				+ "|" + user.ID;

		// �������� ���������
		var response = new List<byte>();
		int bytesRead = 10; // ��� ���������� ������ �� ������ TMP'���

		// ��������� ������ � ������ ������
		// ��� �������� ��������� ������ ���������� ��������� - \n
		byte[] data = Encoding.UTF8.GetBytes(message + '\n');
		// ���������� ������
		await stream.WriteAsync(data);
		// ��������� ������ �� ��������� �������
		while ((bytesRead = stream.ReadByte()) != '\n')
		{
			// ��������� � �����
			response.Add((byte)bytesRead);
		}
		var translation = Encoding.UTF8.GetString(response.ToArray());
		await stream.WriteAsync(Encoding.UTF8.GetBytes(((int)ServerRequests.EndConnection).ToString() + "\n"));

		if (translation == "0") await DisplayAlert("��������", "������: ������ ������������ �� ����������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
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
					table.Columns.Add("��� ������".ToString());
					table.Columns.Add("����� ������".ToString());
					table.Columns.Add("����� ���������".ToString());
					table.Columns.Add("������".ToString());
					table.Columns.Add("��� ������".ToString());
					table.Columns.Add("��� ����������".ToString());

					for (int i = 0; i < counter; i++)
					{
						DataRow dr = table.NewRow();
						dr["��� ������"] = strings[2 + 7 * i];
						dr["����� ������"] = strings[3 + 7 * i];
						dr["����� ���������"] = strings[4 + 7 * i];
						dr["������"] = strings[5 + 7 * i] + " " + strings[6 + 7 * i];
						dr["��� ������"] = strings[7 + 7 * i];
						dr["��� ����������"] = strings[8 + 7 * i];
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