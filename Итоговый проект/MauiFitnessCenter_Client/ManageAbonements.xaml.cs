using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MauiFitnessCenter_Client;

public partial class ManageAbonements : ContentPage
{
	private enum Mode
	{
		Update, Delete, Add
	}

	User user;
	Mode mode = Mode.Add;

	public ManageAbonements(User user_)
	{
		InitializeComponent();

		user = user_;

		if (user.Role == ((int)Roles.Administrator).ToString())
		{
			MenuFlyoutItem deleteToolStripMenuItem = new MenuFlyoutItem();
			deleteToolStripMenuItem.Text = "�������";
			deleteToolStripMenuItem.Clicked += DeleteMenuFlyoutItem_Click;
			ModeToolStripMenuItem.Add(deleteToolStripMenuItem);
		}

		labelTicketCode.IsEnabled = false;
		labelTicketCode.IsVisible = false;
		entryTicketCode.IsEnabled = false;
		entryTicketCode.IsVisible = false;
		buttonFindTicket.IsEnabled = false;
		buttonFindTicket.IsVisible = false;
		LoadManageTicketsInfo();
	}

	private void DeleteMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Delete)
		{
			mode = Mode.Delete;
			labelTitle.Text = "����� �������� ����������";

			labelTicketCode.IsEnabled = true;
			labelTicketCode.IsVisible = true;

			entryTicketCode.Text = string.Empty;

			entryTicketCode.IsEnabled = true;
			entryTicketCode.IsVisible = true;

			buttonFindTicket.IsEnabled = true;
			buttonFindTicket.IsVisible = true;

			entryPrice.Text = string.Empty;
			entryPrice.IsEnabled = false;

			entryTicketDuration.Text = string.Empty;
			entryTicketDuration.IsEnabled = false;

			entryVisitsAmount.Text = string.Empty;
			entryVisitsAmount.IsEnabled = false;

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

	private void AddMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Add)
		{
			mode = Mode.Add;
			labelTitle.Text = "����� ���������� ����������";
			labelTicketCode.IsEnabled = false;
			labelTicketCode.IsVisible = false;
			entryTicketCode.IsEnabled = false;
			entryTicketCode.IsVisible = false;
			buttonFindTicket.IsEnabled = false;
			buttonFindTicket.IsVisible = false;

			entryPrice.Text = string.Empty;
			entryPrice.IsEnabled = true;

			entryTicketDuration.Text = string.Empty;
			entryTicketDuration.IsEnabled = true;

			entryVisitsAmount.Text = string.Empty;
			entryVisitsAmount.IsEnabled = true;

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
			labelTitle.Text = "����� ���������� ����������";
			labelTicketCode.IsEnabled = true;
			labelTicketCode.IsVisible = true;
			entryTicketCode.Text = string.Empty;
			entryTicketCode.IsEnabled = true;
			entryTicketCode.IsVisible = true;
			buttonFindTicket.IsEnabled = true;
			buttonFindTicket.IsVisible = true;

			entryPrice.Text = string.Empty;
			entryPrice.IsEnabled = true;

			entryTicketDuration.Text = string.Empty;
			entryTicketDuration.IsEnabled = true;

			entryVisitsAmount.Text = string.Empty;
			entryVisitsAmount.IsEnabled = true;

			pickerServiceTypes.Title = string.Empty;
			pickerServiceTypes.IsEnabled = true;

			pickerTrainingTypes.Title = string.Empty;
			pickerTrainingTypes.IsEnabled = true;

			buttonEdit.IsEnabled = false;
		}
	}

	async public void LoadManageTicketsInfo()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadManageTicketsInfo).ToString();
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

		if (translation == "0") await DisplayAlert("��������", "������: ������ ���������� �� ����������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			string[] strings = translation.Split('|');
			int counter_train = Convert.ToInt32(strings[1]),
				counter_serv = Convert.ToInt32(strings[2]);

			if (counter_train == 0 || counter_serv == 0)
			{
				await DisplayAlert("��������","������: ������ ������� � ���� ������. ��������� ������ � ��������������", "��");
				await Navigation.PopAsync();
			}
			else
			{
				List<string> trainingTypesItems = new List<string>();
				List<string> serviceTypesItems = new List<string>();
				for (int i = 0; i < counter_train; i++)
				{
					trainingTypesItems.Add(strings[3 + i]);
				}
				for (int i = 0; i < counter_serv; i++)
				{
					serviceTypesItems.Add(strings[3 + counter_train + i]);
				}
				pickerTrainingTypes.ItemsSource = trainingTypesItems;
				pickerServiceTypes.ItemsSource = serviceTypesItems;
			}
		}
	}

	async public void LoadSingleTicketInfo()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadSingleTicket).ToString()
			+ "|" + entryTicketCode.Text;
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

		if (translation == "0") await DisplayAlert("��������", "������: ������ ���������� �� ����������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			string[] strings = translation.Split('|');
			for (int i = 0; i < pickerTrainingTypes.Items.Count; i++)
			{
				if (pickerTrainingTypes.Items[i] == strings[1]) pickerTrainingTypes.SelectedIndex = i;
			}
			//pickerTrainingTypes.Title = strings[1];
			for (int i = 0; i < pickerServiceTypes.Items.Count; i++)
			{
				if (pickerServiceTypes.Items[i] == strings[2]) pickerServiceTypes.SelectedIndex = i;
			}
			//pickerServiceTypes.Title = strings[2];
			entryPrice.Text = strings[3];
			entryVisitsAmount.Text = strings[4];
			entryTicketDuration.Text = strings[5];

			buttonEdit.IsEnabled = true;
		}
	}

	private void ButtonFindTicket_Click(object sender, EventArgs e)
	{
		if (entryTicketCode.Text != string.Empty) LoadSingleTicketInfo();
	}

	private void ButtonEdit_Click(object sender, EventArgs e)
	{
		switch (mode)
		{
			case Mode.Update:
				UpdateTicket();
				break;
			case Mode.Delete:
				DeleteTicket();
				break;
			case Mode.Add:
				AddTicket();
				break;
			default:
				break;
		}
	}

	async public void UpdateTicket()
	{
		if (pickerServiceTypes.SelectedItem.ToString() == "" || pickerTrainingTypes.SelectedItem.ToString() == "" ||
			entryPrice.Text == "" || entryTicketDuration.Text == "" || entryVisitsAmount.Text == "")
		{
			await DisplayAlert("��������", "���� ������ �������� ����������", "��");
			return;
		}

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.UpdateTicket).ToString() + "|" +
			pickerTrainingTypes.SelectedItem.ToString() + "|" + pickerServiceTypes.SelectedItem.ToString() + "|" +
			entryPrice.Text.Replace("|", "]") + "|" + entryVisitsAmount.Text.Replace("|", "]") + "|" +
			entryTicketDuration.Text.Replace("|", "]") + "|" + entryTicketCode.Text.Replace("|", "]");
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

		if (translation == "0") await DisplayAlert("��������", "������: ������ ���������� �� ����������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "��������� ������� �������", "��");
		}
	}

	async public void DeleteTicket()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.DeleteTicket).ToString()
			+ "|" + entryTicketCode.Text;
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

		if (translation == "0") await DisplayAlert("��������", "������: ������ ���������� �� ����������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "��������� ������� �����", "��");
		}
	}

	async public void AddTicket()
	{
		if (pickerServiceTypes.SelectedItem.ToString() == "" || pickerTrainingTypes.SelectedItem.ToString() == "" ||
			entryPrice.Text == "" || entryTicketDuration.Text == "" || entryVisitsAmount.Text == "")
		{
			await DisplayAlert("��������", "���� ������ �������� ����������", "��");
			return;
		}

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.AddTicket).ToString() + "|" +
			pickerTrainingTypes.SelectedItem.ToString() + "|" + pickerServiceTypes.SelectedItem.ToString() + "|" +
			entryPrice.Text.Replace("|", "]") + "|" + entryVisitsAmount.Text.Replace("|", "]") + "|" +
			entryTicketDuration.Text.Replace("|", "]");
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

		if (translation == "0") await DisplayAlert("��������", "������: ������ ���������� �� ����������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "��������� ������� �����", "��");
		}
	}

	private void TextBoxTicketCode_TextChanged(object sender, EventArgs e)
	{
		buttonEdit.IsEnabled = false;
	}
}