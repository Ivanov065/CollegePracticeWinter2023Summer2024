using Microsoft.VisualBasic;
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
using System.Windows.Forms;

namespace Fitness_Center_Client_App
{
	public partial class ManageUsers : Form
	{
		private enum Mode
		{
			Update, Delete, Add
		}

		Form parent;
		User user;
		Mode mode = Mode.Add;

		public ManageUsers(Form parent, User user_)
		{
			InitializeComponent();
			this.parent = parent;
			this.user = user_;

			if (user.Role == ((int)Roles.Administrator).ToString())
			{
				labelUserRole.Enabled = true;
				labelUserRole.Visible = true;
				comboBoxUserRoles.Enabled = true;
				comboBoxUserRoles.Visible = true;
				LoadRoles();
			}
			else
			{
				labelUserRole.Enabled = false;
				labelUserRole.Visible = false;
				comboBoxUserRoles.Enabled = false;
				comboBoxUserRoles.Visible = false;
				textBoxSalary.Enabled = false;
				textBoxSalary.Visible = false;
			}

			labelUserCode.Enabled = false;
			labelUserCode.Visible = false;
			textBoxUserCode.Enabled = false;
			textBoxUserCode.Visible = false;
			buttonFindUser.Enabled = false;
			buttonFindUser.Visible = false;

		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		async public void LoadRoles()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadRoles).ToString();
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

			if (translation == "0") MessageBox.Show("Error: No Roles exists");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				string[] strings = translation.Split('|');
				int counter = Convert.ToInt32(strings[1]);

				for (int i = 0; i < counter; i++)
				{
					comboBoxUserRoles.Items.Add(strings[2 + i]);
				}
				comboBoxUserRoles.Text = comboBoxUserRoles.Items[0].ToString();
			}
		}

		private void AddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Add)
			{
				mode = Mode.Add;

				labelTitle.Text = "Режим Добавления Пользователя";

				labelUserCode.Enabled = false;
				labelUserCode.Visible = false;
				textBoxUserCode.Enabled = false;
				textBoxUserCode.Visible = false;
				textBoxUserCode.Text = string.Empty;
				buttonFindUser.Enabled = false;
				buttonFindUser.Visible = false;

				textBoxName.Text = string.Empty;
				textBoxName.Enabled = true;

				textBoxSurname.Text = string.Empty;
				textBoxSurname.Enabled = true;

				textBoxMiddlename.Text = string.Empty;
				textBoxMiddlename.Enabled = true;

				textBoxAdres.Text = string.Empty;
				textBoxAdres.Enabled = true;

				textBoxPhone.Text = "+7";
				textBoxPhone.Enabled = true;

				dateTimePickerBirthday.Value = new DateTime(2000, 1, 1);
				dateTimePickerBirthday.Text = dateTimePickerBirthday.Value.ToString();
				dateTimePickerBirthday.Enabled = true;

				textBoxLogin.Text = string.Empty;
				textBoxLogin.Enabled = true;

				textBoxPassword.Text = string.Empty;
				textBoxPassword.Enabled = true;

				textBoxPasswordConfirm.Text = string.Empty;
				textBoxPasswordConfirm.Enabled = true;

				buttonEdit.Enabled = true;

				if (user.Role == ((int)Roles.Administrator).ToString())
				{
					labelUserRole.Enabled = true;
					comboBoxUserRoles.Enabled = true;
					textBoxSalary.Text = string.Empty;
					textBoxSalary.Enabled = true;
				}
			}
		}

		private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Update)
			{
				mode = Mode.Update;
				labelTitle.Text = "Режим Обновления Пользователя";

				labelUserCode.Enabled = true;
				labelUserCode.Visible = true;
				textBoxUserCode.Enabled = true;
				textBoxUserCode.Visible = true;
				textBoxUserCode.Text = string.Empty;
				buttonFindUser.Enabled = true;
				buttonFindUser.Visible = true;

				textBoxName.Text = string.Empty;
				textBoxName.Enabled = true;

				textBoxSurname.Text = string.Empty;
				textBoxSurname.Enabled = true;

				textBoxMiddlename.Text = string.Empty;
				textBoxMiddlename.Enabled = true;

				textBoxAdres.Text = string.Empty;
				textBoxAdres.Enabled = true;

				textBoxPhone.Text = "+7";
				textBoxPhone.Enabled = true;

				dateTimePickerBirthday.Value = new DateTime(2000, 1, 1);
				dateTimePickerBirthday.Text = dateTimePickerBirthday.Value.ToString();
				dateTimePickerBirthday.Enabled = true;

				textBoxLogin.Text = string.Empty;
				textBoxLogin.Enabled = true;

				textBoxPassword.Text = string.Empty;
				textBoxPassword.Enabled = true;

				textBoxPasswordConfirm.Text = string.Empty;
				textBoxPasswordConfirm.Enabled = true;

				buttonEdit.Enabled = false;

				if (user.Role == ((int)Roles.Administrator).ToString())
				{
					labelUserRole.Enabled = true;
					comboBoxUserRoles.Enabled = true;
					textBoxSalary.Text = string.Empty;
					textBoxSalary.Enabled = true;
				}
			}
		}

		private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mode != Mode.Delete)
			{
				mode = Mode.Delete;
				labelTitle.Text = "Режим Удаления Пользователя";

				labelUserCode.Enabled = true;
				labelUserCode.Visible = true;
				textBoxUserCode.Enabled = true;
				textBoxUserCode.Visible = true;
				textBoxUserCode.Text = string.Empty;
				buttonFindUser.Enabled = true;
				buttonFindUser.Visible = true;

				textBoxName.Text = string.Empty;
				textBoxName.Enabled = false;

				textBoxSurname.Text = string.Empty;
				textBoxSurname.Enabled = false;

				textBoxMiddlename.Text = string.Empty;
				textBoxMiddlename.Enabled = false;

				textBoxAdres.Text = string.Empty;
				textBoxAdres.Enabled = false;

				textBoxPhone.Text = "+7";
				textBoxPhone.Enabled = false;

				dateTimePickerBirthday.Value = new DateTime(2000, 1, 1);
				dateTimePickerBirthday.Text = dateTimePickerBirthday.Value.ToString();
				dateTimePickerBirthday.Enabled = false;

				textBoxLogin.Text = string.Empty;
				textBoxLogin.Enabled = false;

				textBoxPassword.Text = string.Empty;
				textBoxPassword.Enabled = false;

				textBoxPasswordConfirm.Text = string.Empty;
				textBoxPasswordConfirm.Enabled = false;


				buttonEdit.Enabled = false;

				if (user.Role == ((int)Roles.Administrator).ToString())
				{
					labelUserRole.Enabled = false;
					comboBoxUserRoles.Enabled = false;
					textBoxSalary.Text = string.Empty;
					textBoxSalary.Enabled = false;
				}
			}
		}

		private void ButtonFindUser_Click(object sender, EventArgs e)
		{
			if (textBoxUserCode.Text != "")
			{
				if (user.Role == ((int)Roles.Administrator).ToString()) LoadUser();
				else LoadOnlyClient();
			}
		}

		async public void LoadOnlyClient()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadOnlyClient).ToString() + "|" +
				textBoxUserCode.Text;
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

			if (translation == "0") MessageBox.Show("Error: Client not found");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				string[] strings = translation.Split('|');

				textBoxName.Text = strings[1];
				textBoxSurname.Text = strings[2];
				textBoxMiddlename.Text = strings[3];
				textBoxAdres.Text = strings[4];
				textBoxPhone.Text = strings[5];
				dateTimePickerBirthday.Text = strings[6];
				textBoxLogin.Text = strings[7];
				textBoxPassword.Text = strings[8];
				textBoxPasswordConfirm.Text = strings[8];

				buttonEdit.Enabled = true;
			}
		}

		async public void LoadUser()
		{
			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.LoadUser).ToString() + "|" +
				textBoxUserCode.Text;
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

				textBoxName.Text = strings[1];
				textBoxSurname.Text = strings[2];
				textBoxMiddlename.Text = strings[3];
				textBoxAdres.Text = strings[4];
				textBoxPhone.Text = strings[5];
				dateTimePickerBirthday.Text = strings[6];
				textBoxLogin.Text = strings[7];
				textBoxPassword.Text = strings[8];
				textBoxPasswordConfirm.Text = strings[8];
				comboBoxUserRoles.Text = strings[9];

				if (strings[9] != "Клиент") textBoxSalary.Text = strings[10];


				buttonEdit.Enabled = true;
			}
		}

		private void TextBoxUserCode_TextChanged(object sender, EventArgs e)
		{
			buttonEdit.Enabled = false;
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
			if (!CheckInput()) return;

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.UpdateUser).ToString() + "|" +
				comboBoxUserRoles.Text + "|" + textBoxName.Text + "|" + textBoxSurname.Text + "|" + textBoxMiddlename.Text + "|"
				+ textBoxAdres.Text + "|" + textBoxPhone.Text + "|" + dateTimePickerBirthday.Text + "|"
				+ textBoxLogin.Text + "|" + textBoxPassword.Text + "|" + textBoxUserCode.Text + "|" + textBoxSalary.Text;


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

			if (translation == "0") MessageBox.Show("Error: User was not updated");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("User Updated Successfully");
			}
		}

		async public void DeleteUser()
		{
			if (!CheckInput()) return;

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.DeleteUser).ToString() + "|" + textBoxUserCode.Text;

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

			if (translation == "0") MessageBox.Show("Error: User was not deleted");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("User Deleted Successfully");
			}
		}

		async public void AddUser()
		{
			if (!CheckInput()) return;

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.AddUser).ToString() + "|"
				+ comboBoxUserRoles.Text + "|" + textBoxName.Text + "|" + textBoxSurname.Text + "|" + textBoxMiddlename.Text + "|"
				+ textBoxAdres.Text + "|" + textBoxPhone.Text + "|" + dateTimePickerBirthday.Text + "|"
				+ textBoxLogin.Text + "|" + textBoxPassword.Text + "|" + textBoxSalary.Text;


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

			if (translation == "0") MessageBox.Show("Error: Client was not added");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("User Added Successfully");
			}
		}

		async public void UpdateClient()
		{
			if (!CheckInput()) return;

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.UpdateClient).ToString() + "|" +
				textBoxName.Text + "|" + textBoxSurname.Text + "|" + textBoxMiddlename.Text + "|"
				+ textBoxAdres.Text + "|" + textBoxPhone.Text + "|" + dateTimePickerBirthday.Text + "|"
				+ textBoxLogin.Text + "|" + textBoxPassword.Text + "|" + textBoxUserCode.Text;


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

			if (translation == "0") MessageBox.Show("Error: Client was not updated");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Client Updated Successfully");
			}
		}

		async public void DeleteClient()
		{
			if (!CheckInput()) return;

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.DeleteClient).ToString() + "|" +
				textBoxUserCode.Text;


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

			if (translation == "0") MessageBox.Show("Error: Client was not deleted");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Client Deleted Successfully");
			}
		}

		async public void AddClient()
		{
			if (!CheckInput()) return;

			//Подготавливаем структуру для сообщения
			using TcpClient tcpClient = new TcpClient();
			await tcpClient.ConnectAsync("127.0.0.1", 8888);
			var stream = tcpClient.GetStream();

			// Само сообщение
			string message = ((int)ServerRequests.AddClient).ToString() + "|" +
				textBoxName.Text + "|" + textBoxSurname.Text + "|" + textBoxMiddlename.Text + "|"
				+ textBoxAdres.Text + "|" + textBoxPhone.Text + "|" + dateTimePickerBirthday.Text + "|"
				+ textBoxLogin.Text + "|" + textBoxPassword.Text;


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

			if (translation == "0") MessageBox.Show("Error: Client was not added");
			else if (translation == "error") MessageBox.Show("Error: Error in processing request with DB");
			else
			{
				MessageBox.Show("Client Added Successfully");
			}
		}

		public string PhoneCheck(string str)
		{
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

		public bool CheckInput()
		{
			if (textBoxPassword.Text != textBoxPasswordConfirm.Text)
			{
				MessageBox.Show("Провал Подтверждения Пароля");
				return false;
			}
			if (textBoxName.Text == "" || textBoxSurname.Text == "" || textBoxMiddlename.Text == ""
				|| textBoxLogin.Text == "" || textBoxAdres.Text == "" || textBoxPassword.Text == "")
			{
				MessageBox.Show("Ввод Пустых Значений Недопустим");
				return false;
			}
			textBoxPhone.Text = PhoneCheck(textBoxPhone.Text);
			if (textBoxPhone.Text.Length != 12)
			{
				MessageBox.Show("Неправильный Ввод Номер Телефона");
				return false;
			}
			return true;
		}

		private void ComboBoxUserRoles_TextChanged(object sender, EventArgs e)
		{
			if (comboBoxUserRoles.Text == "Клиент")
			{
				textBoxSalary.Enabled = false;
			}
			else
			{
				if (user.Role == ((int)Roles.Administrator).ToString())
				{
					textBoxSalary.Enabled = true;
				}
			}
		}
	}
}
