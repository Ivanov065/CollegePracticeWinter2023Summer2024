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
		//�������������� ��������� ��� ���������
		using TcpClient tcpClient = new TcpClient();
		await tcpClient.ConnectAsync("127.0.0.1", 8888);
		var stream = tcpClient.GetStream();

		// ���� ���������
		string message = ((int)ServerRequests.LoadTickets).ToString();

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
			int counter = Convert.ToInt32(strings[1]);
			dataGridTickets.ItemsSource = null;
			if (counter > 0)
			{
				DataTable table = new DataTable();
				table.Columns.Add("��� ����������".ToString());
				table.Columns.Add("��� ����������".ToString());
				table.Columns.Add("��� ������".ToString());
				table.Columns.Add("����".ToString());
				table.Columns.Add("���-�� ���������".ToString());
				table.Columns.Add("������� (����)".ToString());

				for (int i = 0; i < counter; i++)
				{
					DataRow dr = table.NewRow();
					dr["��� ����������"] = strings[2 + 6 * i];
					dr["��� ����������"] = strings[3 + 6 * i];
					dr["��� ������"] = strings[4 + 6 * i];
					dr["����"] = strings[5 + 6 * i];
					dr["���-�� ���������"] = strings[6 + 6 * i];
					dr["������� (����)"] = strings[7 + 6 * i];
					table.Rows.Add(dr);
				}
				dataGridTickets.ItemsSource = table;
			}
		}
	}
}