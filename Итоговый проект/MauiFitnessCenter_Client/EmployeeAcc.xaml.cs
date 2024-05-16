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

public partial class EmployeeAcc : ContentPage
{
	User user;

	public EmployeeAcc(User user)
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
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadEmployee) + "|";
		message += user.ID + "|" + user.Role;
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

			labelFIO.Text += strings[1] + " " + strings[2] + " " + strings[3];
			labelAdres.Text += strings[4];
			labelPhone.Text += strings[5];
			labelBirthday.Text += strings[6];
			labelRole.Text += strings[7];
			labelSalary.Text += strings[8];
		}
	}
}