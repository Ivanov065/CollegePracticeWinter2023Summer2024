using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness_Center_Client_App
{

	public partial class ManageAbonements : Form
	{
		private enum Mode
		{
			Update, Delete, Add
		}

		Form parent;
		User user;
		Mode mode = Mode.Add;
		public ManageAbonements(Form parent, User user_)
		{
			InitializeComponent();
			this.parent = parent;
			user = user_;

			if (user.Role == ((int)Roles.Administrator).ToString())
			{
				ToolStripMenuItem deleteToolStripMenuItem = new ToolStripMenuItem();
				deleteToolStripMenuItem.Text = "Удалить";
				deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
				deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
				ModeToolStripMenuItem.DropDown.Items.Add(deleteToolStripMenuItem);
			}

			labelTicketCode.Enabled = false;
			labelTicketCode.Visible = false;
			textBoxTicketCode.Enabled = false;
			textBoxTicketCode.Visible = false;
			buttonFindTicket.Enabled = false;
			buttonFindTicket.Visible = false;
			LoadManageTicketsInfo();
		}

		private void DeleteToolStripMenuItem_Click(object? sender, EventArgs e)
		{
			if (mode != Mode.Delete)
			{
				mode = Mode.Delete;
				labelTitle.Text = "Режим Удаления Абонемента";

				labelTicketCode.Enabled = true;
				labelTicketCode.Visible = true;

				textBoxTicketCode.Text = string.Empty;

				textBoxTicketCode.Enabled = true;
				textBoxTicketCode.Visible = true;

				buttonFindTicket.Enabled = true;
				buttonFindTicket.Visible = true;

				textBoxPrice.Text = string.Empty;
				textBoxPrice.Enabled = false;

				textBoxTicketDuration.Text = string.Empty;
				textBoxTicketDuration.Enabled = false;

				textBoxVisitsAmount.Text = string.Empty;
				textBoxVisitsAmount.Enabled = false;

				comboBoxServiceTypes.Text = string.Empty;
				comboBoxServiceTypes.Enabled = false;

				comboBoxTrainingTypes.Text = string.Empty;
				comboBoxTrainingTypes.Enabled = false;

				buttonEdit.Enabled = false;
			}
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void AddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Add)
			{
				mode = Mode.Add;
				labelTitle.Text = "Режим Добавления Абонемента";
				labelTicketCode.Enabled = false;
				labelTicketCode.Visible = false;
				textBoxTicketCode.Enabled = false;
				textBoxTicketCode.Visible = false;
				buttonFindTicket.Enabled = false;
				buttonFindTicket.Visible = false;

				textBoxPrice.Text = string.Empty;
				textBoxPrice.Enabled = true;

				textBoxTicketDuration.Text = string.Empty;
				textBoxTicketDuration.Enabled = true;

				textBoxVisitsAmount.Text = string.Empty;
				textBoxVisitsAmount.Enabled = true;

				comboBoxServiceTypes.Text = string.Empty;
				comboBoxServiceTypes.Enabled = true;

				comboBoxTrainingTypes.Text = string.Empty;
				comboBoxTrainingTypes.Enabled = true;

				buttonEdit.Enabled = true;
			}
		}

		private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Update)
			{
				mode = Mode.Update;
				labelTitle.Text = "Режим Обновления Абонемента";
				labelTicketCode.Enabled = true;
				labelTicketCode.Visible = true;
				textBoxTicketCode.Text = string.Empty;
				textBoxTicketCode.Enabled = true;
				textBoxTicketCode.Visible = true;
				buttonFindTicket.Enabled = true;
				buttonFindTicket.Visible = true;

				textBoxPrice.Text = string.Empty;
				textBoxPrice.Enabled = true;

				textBoxTicketDuration.Text = string.Empty;
				textBoxTicketDuration.Enabled = true;

				textBoxVisitsAmount.Text = string.Empty;
				textBoxVisitsAmount.Enabled = true;

				comboBoxServiceTypes.Text = string.Empty;
				comboBoxServiceTypes.Enabled = true;

				comboBoxTrainingTypes.Text = string.Empty;
				comboBoxTrainingTypes.Enabled = true;

				buttonEdit.Enabled = false;
			}
		}

		async public void LoadManageTicketsInfo()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadManageTicketsInfo).ToString();
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

			if (translation == "0") MessageBox.Show("Error: User not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				string[] strings = translation.Split('|');
				int counter_train = Convert.ToInt32(strings[1]),
					counter_serv = Convert.ToInt32(strings[2]);

				if (counter_train == 0 || counter_serv == 0)
				{
					MessageBox.Show("Error: Empty tables in DB. Ask administrator for help.");
					this.Close();
				}
				else
				{
					for (int i = 0; i < counter_train; i++)
					{
						comboBoxTrainingTypes.Items.Add(strings[3 + i]);
					}
					for (int i = 0; i < counter_serv; i++)
					{
						comboBoxServiceTypes.Items.Add(strings[3 + counter_train + i]);
					}
				}
			}
		}

		async public void LoadSingleTicketInfo()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadSingleTicket).ToString()
				+ "|" + textBoxTicketCode.Text;
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

			if (translation == "0") MessageBox.Show("Error: Abonement not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				string[] strings = translation.Split('|');
				comboBoxTrainingTypes.Text = strings[1];
				comboBoxServiceTypes.Text = strings[2];
				textBoxPrice.Text = strings[3];
				textBoxVisitsAmount.Text = strings[4];
				textBoxTicketDuration.Text = strings[5];

				buttonEdit.Enabled = true;
			}
		}

		private void ButtonFindTicket_Click(object sender, EventArgs e)
		{
			if (textBoxTicketCode.Text != string.Empty) LoadSingleTicketInfo();
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
			if (comboBoxServiceTypes.Text == "" || comboBoxTrainingTypes.Text == "" ||
				textBoxPrice.Text == "" || textBoxTicketDuration.Text == "" || textBoxVisitsAmount.Text == "")
			{
				MessageBox.Show("Ввод пустых значений недопустим");
				return;
			}

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.UpdateTicket).ToString() + "|" +
				comboBoxTrainingTypes.Text + "|" + comboBoxServiceTypes.Text + "|" +
				textBoxPrice.Text.Replace("|", "]") + "|" + textBoxVisitsAmount.Text.Replace("|", "]") + "|" +
				textBoxTicketDuration.Text.Replace("|", "]") + "|" + textBoxTicketCode.Text.Replace("|", "]");
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

			if (translation == "0") MessageBox.Show("Error: Ticket not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Ticket Updated successfully")
;
			}
		}

		async public void DeleteTicket()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.DeleteTicket).ToString()
				+ "|" + textBoxTicketCode.Text;
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

			if (translation == "0") MessageBox.Show("Error: Ticket not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Ticket Deleted Successfully");
			}
		}

		async public void AddTicket()
		{
			if (comboBoxServiceTypes.Text == "" || comboBoxTrainingTypes.Text == "" ||
				textBoxPrice.Text == "" || textBoxTicketDuration.Text == "" || textBoxVisitsAmount.Text == "")
			{
				MessageBox.Show("Ввод пустых значений недопустим");
				return;
			}

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.AddTicket).ToString() + "|" +
				comboBoxTrainingTypes.Text + "|" + comboBoxServiceTypes.Text + "|" +
				textBoxPrice.Text.Replace("|", "]") + "|" + textBoxVisitsAmount.Text.Replace("|", "]") + "|" +
				textBoxTicketDuration.Text.Replace("|", "]");
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

			if (translation == "0") MessageBox.Show("Error: User not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Ticket Added Successfully");
			}
		}

		private void TextBoxTicketCode_TextChanged(object sender, EventArgs e)
		{
			buttonEdit.Enabled = false;
		}
	}
}
