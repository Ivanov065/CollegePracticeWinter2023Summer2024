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

public partial class ShowClients : ContentPage
{
	public ShowClients()
	{
		InitializeComponent();
		LoadClients();
	}

	private async void ButtonExit_Click(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	async public void LoadClients()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		string message = ((int)ServerRequests.LoadClients).ToString();

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
				DataTable table = new DataTable();
				table.Columns.Add("ID �������".ToString());
				table.Columns.Add("��� �������".ToString());
				table.Columns.Add("���� ��������".ToString());
				table.Columns.Add("�����".ToString());
				table.Columns.Add("�������".ToString());

				for (int i = 0; i < counter; i++)
				{
					DataRow dr = table.NewRow();
					dr["ID �������"] = strings[2 + 7 * i];
					dr["��� �������"] = strings[3 + 7 * i] + " " + strings[4 + 7 * i] + " " + strings[5 + 7 * i];
					dr["���� ��������"] = strings[6 + 7 * i];
					dr["�����"] = strings[7 + 7 * i];
					dr["�������"] = strings[8 + 7 * i];
					table.Rows.Add(dr);
				}
				dataGridClients.ItemsSource = null;
				dataGridClients.ItemsSource = table;
			}
		}
	}

	private void ButtonFindTicket_Click(object sender, EventArgs e)
	{
		FindTicket();
	}

	async public void FindTicket()
	{
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadActiveTicket) + "|";
		message += entryClientCode.Text;
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
			if (strings[1] == "1")
			{
				labelTicketId.Text = "�������� ��������� ����� " + strings[2];
				labelTicketTemplateCode.Text = "��� ������� ����������: " + strings[3];
				labelClientCode.Text = "��� �������: " + strings[4];
				labelTimeActive.Text = "����� ������: " + strings[5] + " - " + strings[6];
				labelLastVisits.Text = "�������� ���������: " + strings[7];
				labelTrainingType.Text = "��� ����������: " + strings[8];
				labelServiceType.Text = "��� ������: " + strings[9];
			}
			else
			{
				labelTicketId.Text = "��������� �� ��� ������";
				labelTicketTemplateCode.Text = "��� ������� ����������: ";
				labelClientCode.Text = "��� �������: ";
				labelTimeActive.Text = "����� ������: ";
				labelLastVisits.Text = "�������� ���������: ";
				labelTrainingType.Text = "��� ����������: ";
				labelServiceType.Text = "��� ������: ";
			}
		}
	}
}