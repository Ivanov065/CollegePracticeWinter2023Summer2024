using Microsoft.VisualBasic;
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MauiFitnessCenter_Client;

public partial class ManageUsers : ContentPage
{
	private enum Mode
	{
		Update, Delete, Add
	}

	User user;
	Mode mode = Mode.Add;

	public ManageUsers(User user_)
	{
		InitializeComponent();
		this.user = user_;

		if (user.Role == ((int)Roles.Administrator).ToString())
		{
			labelUserRole.IsEnabled = true;
			labelUserRole.IsVisible = true;
			pickerUserRoles.IsEnabled = true;
			pickerUserRoles.IsVisible = true;
			LoadRoles();
		}
		else
		{
			labelUserRole.IsEnabled = false;
			labelUserRole.IsVisible = false;
			pickerUserRoles.IsEnabled = false;
			pickerUserRoles.IsVisible = false;
			entrySalary.IsEnabled = false;
			entrySalary.IsVisible = false;
		}

		labelUserCode.IsEnabled = false;
		labelUserCode.IsVisible = false;
		entryUserCode.IsEnabled = false;
		entryUserCode.IsVisible = false;
		buttonFindUser.IsEnabled = false;
		buttonFindUser.IsVisible = false;

	}

	private async void ButtonExit_Click(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	async public void LoadRoles()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadRoles).ToString();
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

		if (translation == "0") await DisplayAlert("��������", "������: ���� ������������� �� �������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			string[] strings = translation.Split('|');
			int counter = Convert.ToInt32(strings[1]);
			List<string> items = new List<string>();
			for (int i = 0; i < counter; i++)
			{
				items.Add(strings[2 + i]);
			}
			pickerUserRoles.ItemsSource = items;
			pickerUserRoles.SelectedIndex = 0;
		}
	}

	private void AddMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Add)
		{
			mode = Mode.Add;

			labelTitle.Text = "����� ���������� ������������";

			labelUserCode.IsEnabled = false;
			labelUserCode.IsVisible = false;
			entryUserCode.IsEnabled = false;
			entryUserCode.IsVisible = false;
			entryUserCode.Text = string.Empty;
			buttonFindUser.IsEnabled = false;
			buttonFindUser.IsVisible = false;

			entryName.Text = string.Empty;
			entryName.IsEnabled = true;

			entrySurname.Text = string.Empty;
			entrySurname.IsEnabled = true;

			entryMiddlename.Text = string.Empty;
			entryMiddlename.IsEnabled = true;

			entryAdres.Text = string.Empty;
			entryAdres.IsEnabled = true;

			entryPhone.Text = "+7";
			entryPhone.IsEnabled = true;

			datePickerBirthday.Date = new DateTime(2000, 1, 1);
			datePickerBirthday.IsEnabled = true;

			entryLogin.Text = string.Empty;
			entryLogin.IsEnabled = true;

			entryPassword.Text = string.Empty;
			entryPassword.IsEnabled = true;

			entryPasswordConfirm.Text = string.Empty;
			entryPasswordConfirm.IsEnabled = true;

			buttonEdit.IsEnabled = true;

			if (user.Role == ((int)Roles.Administrator).ToString())
			{
				labelUserRole.IsEnabled = true;
				pickerUserRoles.IsEnabled = true;
				entrySalary.Text = string.Empty;
				entrySalary.IsEnabled = true;
			}
		}
	}

	private void UpdateMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Update)
		{
			mode = Mode.Update;
			labelTitle.Text = "����� ���������� ������������";

			labelUserCode.IsEnabled = true;
			labelUserCode.IsVisible = true;
			entryUserCode.IsEnabled = true;
			entryUserCode.IsVisible = true;
			entryUserCode.Text = string.Empty;
			buttonFindUser.IsEnabled = true;
			buttonFindUser.IsVisible = true;

			entryName.Text = string.Empty;
			entryName.IsEnabled = true;

			entrySurname.Text = string.Empty;
			entrySurname.IsEnabled = true;

			entryMiddlename.Text = string.Empty;
			entryMiddlename.IsEnabled = true;

			entryAdres.Text = string.Empty;
			entryAdres.IsEnabled = true;

			entryPhone.Text = "+7";
			entryPhone.IsEnabled = true;

			datePickerBirthday.Date = new DateTime(2000, 1, 1);
			datePickerBirthday.IsEnabled = true;

			entryLogin.Text = string.Empty;
			entryLogin.IsEnabled = true;

			entryPassword.Text = string.Empty;
			entryPassword.IsEnabled = true;

			entryPasswordConfirm.Text = string.Empty;
			entryPasswordConfirm.IsEnabled = true;

			buttonEdit.IsEnabled = false;

			if (user.Role == ((int)Roles.Administrator).ToString())
			{
				labelUserRole.IsEnabled = true;
				pickerUserRoles.IsEnabled = true;
				entrySalary.Text = string.Empty;
				entrySalary.IsEnabled = true;
			}
		}
	}

	private void DeleteMenuFlyoutItem_Click(object sender, EventArgs e)
	{
		if (mode != Mode.Delete)
		{
			mode = Mode.Delete;
			labelTitle.Text = "����� �������� ������������";

			labelUserCode.IsEnabled = true;
			labelUserCode.IsVisible = true;
			entryUserCode.IsEnabled = true;
			entryUserCode.IsVisible = true;
			entryUserCode.Text = string.Empty;
			buttonFindUser.IsEnabled = true;
			buttonFindUser.IsVisible = true;

			entryName.Text = string.Empty;
			entryName.IsEnabled = false;

			entrySurname.Text = string.Empty;
			entrySurname.IsEnabled = false;

			entryMiddlename.Text = string.Empty;
			entryMiddlename.IsEnabled = false;

			entryAdres.Text = string.Empty;
			entryAdres.IsEnabled = false;

			entryPhone.Text = "+7";
			entryPhone.IsEnabled = false;

			datePickerBirthday.Date = new DateTime(2000, 1, 1);
			datePickerBirthday.IsEnabled = false;

			entryLogin.Text = string.Empty;
			entryLogin.IsEnabled = false;

			entryPassword.Text = string.Empty;
			entryPassword.IsEnabled = false;

			entryPasswordConfirm.Text = string.Empty;
			entryPasswordConfirm.IsEnabled = false;


			buttonEdit.IsEnabled = false;

			if (user.Role == ((int)Roles.Administrator).ToString())
			{
				labelUserRole.IsEnabled = false;
				pickerUserRoles.IsEnabled = false;
				entrySalary.Text = string.Empty;
				entrySalary.IsEnabled = false;
			}
		}
	}

	private void ButtonFindUser_Click(object sender, EventArgs e)
	{
		if (entryUserCode.Text != "")
		{
			if (user.Role == ((int)Roles.Administrator).ToString()) LoadUser();
			else LoadOnlyClient();
		}
	}

	async public void LoadOnlyClient()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadOnlyClient).ToString() + "|" +
			entryUserCode.Text;
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

			entryName.Text = strings[1];
			entrySurname.Text = strings[2];
			entryMiddlename.Text = strings[3];
			entryAdres.Text = strings[4];
			entryPhone.Text = strings[5];
			datePickerBirthday.Date = DateTime.Parse(strings[6]);
			entryLogin.Text = strings[7];
			entryPassword.Text = strings[8];
			entryPasswordConfirm.Text = strings[8];

			buttonEdit.IsEnabled = true;
		}
	}

	async public void LoadUser()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadUser).ToString() + "|" +
			entryUserCode.Text;
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

			entryName.Text = strings[1];
			entrySurname.Text = strings[2];
			entryMiddlename.Text = strings[3];
			entryAdres.Text = strings[4];
			entryPhone.Text = strings[5];
			datePickerBirthday.Date = DateTime.Parse(strings[6]);
			entryLogin.Text = strings[7];
			entryPassword.Text = strings[8];
			entryPasswordConfirm.Text = strings[8];
			for (int i = 0; i < pickerUserRoles.Items.Count; i++)
			{
				if (pickerUserRoles.Items[i] == strings[9]) pickerUserRoles.SelectedIndex = i;
			}

			if (strings[9] != "������") entrySalary.Text = strings[10];


			buttonEdit.IsEnabled = true;
		}
	}

	private void TextBoxUserCode_TextChanged(object sender, EventArgs e)
	{
		buttonEdit.IsEnabled = false;
	}

	private void ButtonEdit_Click(object sender, EventArgs e)
	{
		if (user.Role == ((int)Roles.Administrator).ToString())
		{
			switch (mode)
			{
				case Mode.Update:
					UpdateUser();
					break;
				case Mode.Delete:
					DeleteUser();
					break;
				case Mode.Add:
					AddUser();
					break;
				default:
					break;
			}
		}
		else
		{
			switch (mode)
			{
				case Mode.Update:
					UpdateClient();
					break;
				case Mode.Delete:
					DeleteClient();
					break;
				case Mode.Add:
					AddClient();
					break;
				default:
					break;
			}
		}
	}

	async public void UpdateUser()
	{
		if (!await CheckInput()) return;

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.UpdateUser).ToString() + "|" +
			pickerUserRoles.SelectedItem.ToString() + "|" + entryName.Text + "|" + entrySurname.Text + "|" + entryMiddlename.Text + "|"
			+ entryAdres.Text + "|" + entryPhone.Text + "|" + datePickerBirthday.Date.ToString() + "|"
			+ entryLogin.Text + "|" + entryPassword.Text + "|" + entryUserCode.Text + "|" + entrySalary.Text;


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
			await DisplayAlert("�����", "������������ ������� �������", "��");
		}
	}

	async public void DeleteUser()
	{
		if (! await CheckInput()) return;

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.DeleteUser).ToString() + "|" + entryUserCode.Text;

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

		if (translation == "0") await DisplayAlert("��������", "������: ������������ �� ������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "������������ ������� �����", "��");
		}
	}

	async public void AddUser()
	{
		if (! await CheckInput()) return;

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.AddUser).ToString() + "|"
			+ pickerUserRoles.SelectedItem.ToString() + "|" + entryName.Text + "|" + entrySurname.Text + "|" + entryMiddlename.Text + "|"
			+ entryAdres.Text + "|" + entryPhone.Text + "|" + datePickerBirthday.Date.ToString() + "|"
			+ entryLogin.Text + "|" + entryPassword.Text + "|" + entrySalary.Text;


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
			await DisplayAlert("�����", "������������ ������� ��������", "��");
		}
	}

	async public void UpdateClient()
	{
		if (! await CheckInput()) return;

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.UpdateClient).ToString() + "|" +
			entryName.Text + "|" + entrySurname.Text + "|" + entryMiddlename.Text + "|"
			+ entryAdres.Text + "|" + entryPhone.Text + "|" + datePickerBirthday.Date.ToString() + "|"
			+ entryLogin.Text + "|" + entryPassword.Text + "|" + entryUserCode.Text;


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

		if (translation == "0") await DisplayAlert("��������", "������: ������ �� ��� �������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "������ ������� ���������", "��");
		}
	}

	async public void DeleteClient()
	{
		if (! await CheckInput()) return;

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.DeleteClient).ToString() + "|" +
			entryUserCode.Text;


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

		if (translation == "0") await DisplayAlert("��������", "������: ������ �� ��� �������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "������ ������� �����", "��");
		}
	}

	async public void AddClient()
	{
		if (!await CheckInput()) return;

		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.AddClient).ToString() + "|" +
			entryName.Text + "|" + entrySurname.Text + "|" + entryMiddlename.Text + "|"
			+ entryAdres.Text + "|" + entryPhone.Text + "|" + datePickerBirthday.Date.ToString() + "|"
			+ entryLogin.Text + "|" + entryPassword.Text;


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

		if (translation == "0") await DisplayAlert("��������", "������: ������ �� ��� ��������", "��");
		else if (translation == "error") await DisplayAlert("��������", "������: �������� � ���������� ������� �� ���� ������", "��");
		else
		{
			await DisplayAlert("�����", "������ ������� ��������", "��");
		}
	}

	public string PhoneCheck(string str)
	{
		if (str == null) return null;
		if (str[0] != '+') return null;

		string tmp = "+";
		for (int i = 1; i < str.Length; i++)
		{
			if (str[i] == '0' || str[i] == '1' || str[i] == '2' || str[i] == '3' ||
				str[i] == '4' || str[i] == '5' || str[i] == '6' || str[i] == '7' ||
				str[i] == '8' || str[i] == '9')
			{
				tmp += str[i];
			}
		}
		return tmp;
	}

	public async Task<bool> CheckInput()
	{
		if (entryPassword.Text != entryPasswordConfirm.Text)
		{
			await DisplayAlert("��������", "������ ������������� ������", "��");
			return false;
		}
		if (entryName.Text == "" || entrySurname.Text == "" || entryMiddlename.Text == ""
			|| entryLogin.Text == "" || entryAdres.Text == "" || entryPassword.Text == "")
		{
			await DisplayAlert("��������", "���� ������ �������� ����������", "��");
			return false;
		}
		entryPhone.Text = PhoneCheck(entryPhone.Text);
		if (entryPhone.Text == null || entryPhone.Text.Length != 12)
		{
			await DisplayAlert("��������", "������������ ���� ����� ��������", "��");
			return false;
		}
		return true;
	}

	private void ComboBoxUserRoles_TextChanged(object sender, EventArgs e)
	{
		if (pickerUserRoles.Title == "������")
		{
			entrySalary.IsEnabled = false;
		}
		else
		{
			if (user.Role == ((int)Roles.Administrator).ToString())
			{
				entrySalary.IsEnabled = true;
			}
		}
	}
}