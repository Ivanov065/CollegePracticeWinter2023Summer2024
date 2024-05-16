namespace Fitness_Center_Client_App
{
	partial class ManageUsers
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			menuStripWorkMode = new MenuStrip();
			ModeToolStripMenuItem = new ToolStripMenuItem();
			AddToolStripMenuItem = new ToolStripMenuItem();
			UpdateToolStripMenuItem = new ToolStripMenuItem();
			DeleteToolStripMenuItem = new ToolStripMenuItem();
			labelTitle = new Label();
			textBoxName = new TextBox();
			label3 = new Label();
			textBoxSurname = new TextBox();
			label1 = new Label();
			textBoxMiddlename = new TextBox();
			label2 = new Label();
			label4 = new Label();
			textBoxAdres = new TextBox();
			label5 = new Label();
			textBoxPhone = new TextBox();
			label6 = new Label();
			textBoxLogin = new TextBox();
			label7 = new Label();
			textBoxPassword = new TextBox();
			label8 = new Label();
			textBoxPasswordConfirm = new TextBox();
			label9 = new Label();
			textBoxUserCode = new TextBox();
			labelUserCode = new Label();
			labelUserRole = new Label();
			buttonFindUser = new Button();
			dateTimePickerBirthday = new DateTimePicker();
			comboBoxUserRoles = new ComboBox();
			buttonExit = new Button();
			buttonEdit = new Button();
			textBoxSalary = new TextBox();
			labelSalary = new Label();
			menuStripWorkMode.SuspendLayout();
			SuspendLayout();
			// 
			// menuStripWorkMode
			// 
			menuStripWorkMode.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			menuStripWorkMode.ImageScalingSize = new Size(20, 20);
			menuStripWorkMode.Items.AddRange(new ToolStripItem[] { ModeToolStripMenuItem });
			menuStripWorkMode.Location = new Point(0, 0);
			menuStripWorkMode.Name = "menuStripWorkMode";
			menuStripWorkMode.Size = new Size(1205, 31);
			menuStripWorkMode.TabIndex = 2;
			menuStripWorkMode.Text = "menuStrip1";
			// 
			// ModeToolStripMenuItem
			// 
			ModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AddToolStripMenuItem, UpdateToolStripMenuItem, DeleteToolStripMenuItem });
			ModeToolStripMenuItem.Name = "ModeToolStripMenuItem";
			ModeToolStripMenuItem.Size = new Size(78, 27);
			ModeToolStripMenuItem.Text = "Режим";
			// 
			// AddToolStripMenuItem
			// 
			AddToolStripMenuItem.Name = "AddToolStripMenuItem";
			AddToolStripMenuItem.Size = new Size(172, 28);
			AddToolStripMenuItem.Text = "Добавить";
			AddToolStripMenuItem.Click += AddToolStripMenuItem_Click;
			// 
			// UpdateToolStripMenuItem
			// 
			UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
			UpdateToolStripMenuItem.Size = new Size(172, 28);
			UpdateToolStripMenuItem.Text = "Обновить";
			UpdateToolStripMenuItem.Click += UpdateToolStripMenuItem_Click;
			// 
			// DeleteToolStripMenuItem
			// 
			DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
			DeleteToolStripMenuItem.Size = new Size(172, 28);
			DeleteToolStripMenuItem.Text = "Удалить";
			DeleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
			// 
			// labelTitle
			// 
			labelTitle.AutoSize = true;
			labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			labelTitle.Location = new Point(392, 40);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new Size(486, 41);
			labelTitle.TabIndex = 16;
			labelTitle.Text = "Режим Добавления Пользователя";
			// 
			// textBoxName
			// 
			textBoxName.Location = new Point(168, 96);
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new Size(184, 27);
			textBoxName.TabIndex = 18;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(112, 96);
			label3.Name = "label3";
			label3.Size = new Size(55, 28);
			label3.TabIndex = 17;
			label3.Text = "Имя:";
			// 
			// textBoxSurname
			// 
			textBoxSurname.Location = new Point(168, 152);
			textBoxSurname.Name = "textBoxSurname";
			textBoxSurname.Size = new Size(184, 27);
			textBoxSurname.TabIndex = 20;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(64, 152);
			label1.Name = "label1";
			label1.Size = new Size(100, 28);
			label1.TabIndex = 19;
			label1.Text = "Фамилия:";
			// 
			// textBoxMiddlename
			// 
			textBoxMiddlename.Location = new Point(168, 208);
			textBoxMiddlename.Name = "textBoxMiddlename";
			textBoxMiddlename.Size = new Size(184, 27);
			textBoxMiddlename.TabIndex = 22;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(64, 208);
			label2.Name = "label2";
			label2.Size = new Size(100, 28);
			label2.TabIndex = 21;
			label2.Text = "Отчество:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(8, 264);
			label4.Name = "label4";
			label4.Size = new Size(156, 28);
			label4.TabIndex = 23;
			label4.Text = "Дата Рождения:";
			// 
			// textBoxAdres
			// 
			textBoxAdres.Location = new Point(168, 320);
			textBoxAdres.Name = "textBoxAdres";
			textBoxAdres.Size = new Size(184, 27);
			textBoxAdres.TabIndex = 26;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label5.Location = new Point(88, 320);
			label5.Name = "label5";
			label5.Size = new Size(71, 28);
			label5.TabIndex = 25;
			label5.Text = "Адрес:";
			// 
			// textBoxPhone
			// 
			textBoxPhone.Location = new Point(584, 96);
			textBoxPhone.Name = "textBoxPhone";
			textBoxPhone.Size = new Size(184, 27);
			textBoxPhone.TabIndex = 28;
			textBoxPhone.Text = "+7";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label6.Location = new Point(480, 96);
			label6.Name = "label6";
			label6.Size = new Size(95, 28);
			label6.TabIndex = 27;
			label6.Text = "Телефон:";
			// 
			// textBoxLogin
			// 
			textBoxLogin.Location = new Point(584, 152);
			textBoxLogin.Name = "textBoxLogin";
			textBoxLogin.Size = new Size(184, 27);
			textBoxLogin.TabIndex = 30;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label7.Location = new Point(504, 152);
			label7.Name = "label7";
			label7.Size = new Size(78, 28);
			label7.TabIndex = 29;
			label7.Text = "Логин: ";
			// 
			// textBoxPassword
			// 
			textBoxPassword.Location = new Point(584, 208);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.PasswordChar = '*';
			textBoxPassword.Size = new Size(184, 27);
			textBoxPassword.TabIndex = 32;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label8.Location = new Point(488, 208);
			label8.Name = "label8";
			label8.Size = new Size(90, 28);
			label8.TabIndex = 31;
			label8.Text = "Пароль: ";
			// 
			// textBoxPasswordConfirm
			// 
			textBoxPasswordConfirm.Location = new Point(584, 264);
			textBoxPasswordConfirm.Name = "textBoxPasswordConfirm";
			textBoxPasswordConfirm.PasswordChar = '*';
			textBoxPasswordConfirm.Size = new Size(184, 27);
			textBoxPasswordConfirm.TabIndex = 34;
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label9.Location = new Point(424, 264);
			label9.Name = "label9";
			label9.Size = new Size(148, 28);
			label9.TabIndex = 33;
			label9.Text = "Пароль подтв.:";
			// 
			// textBoxUserCode
			// 
			textBoxUserCode.Location = new Point(952, 96);
			textBoxUserCode.Name = "textBoxUserCode";
			textBoxUserCode.Size = new Size(80, 27);
			textBoxUserCode.TabIndex = 36;
			textBoxUserCode.TextChanged += TextBoxUserCode_TextChanged;
			// 
			// labelUserCode
			// 
			labelUserCode.AutoSize = true;
			labelUserCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelUserCode.Location = new Point(832, 96);
			labelUserCode.Name = "labelUserCode";
			labelUserCode.Size = new Size(119, 28);
			labelUserCode.TabIndex = 35;
			labelUserCode.Text = "Код польз.: ";
			// 
			// labelUserRole
			// 
			labelUserRole.AutoSize = true;
			labelUserRole.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelUserRole.Location = new Point(824, 208);
			labelUserRole.Name = "labelUserRole";
			labelUserRole.Size = new Size(128, 28);
			labelUserRole.TabIndex = 37;
			labelUserRole.Text = "Роль польз.: ";
			// 
			// buttonFindUser
			// 
			buttonFindUser.Location = new Point(1040, 88);
			buttonFindUser.Name = "buttonFindUser";
			buttonFindUser.Size = new Size(144, 40);
			buttonFindUser.TabIndex = 39;
			buttonFindUser.Text = "Найти";
			buttonFindUser.UseVisualStyleBackColor = true;
			buttonFindUser.Click += ButtonFindUser_Click;
			// 
			// dateTimePickerBirthday
			// 
			dateTimePickerBirthday.Location = new Point(168, 264);
			dateTimePickerBirthday.Name = "dateTimePickerBirthday";
			dateTimePickerBirthday.Size = new Size(184, 27);
			dateTimePickerBirthday.TabIndex = 40;
			dateTimePickerBirthday.Value = new DateTime(2000, 1, 1, 0, 0, 0, 0);
			// 
			// comboBoxUserRoles
			// 
			comboBoxUserRoles.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxUserRoles.FormattingEnabled = true;
			comboBoxUserRoles.Location = new Point(944, 208);
			comboBoxUserRoles.Name = "comboBoxUserRoles";
			comboBoxUserRoles.Size = new Size(184, 28);
			comboBoxUserRoles.TabIndex = 41;
			comboBoxUserRoles.TextChanged += ComboBoxUserRoles_TextChanged;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(16, 376);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(160, 48);
			buttonExit.TabIndex = 42;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// buttonEdit
			// 
			buttonEdit.Location = new Point(1024, 376);
			buttonEdit.Name = "buttonEdit";
			buttonEdit.Size = new Size(160, 48);
			buttonEdit.TabIndex = 43;
			buttonEdit.Text = "Выполнить";
			buttonEdit.UseVisualStyleBackColor = true;
			buttonEdit.Click += ButtonEdit_Click;
			// 
			// textBoxSalary
			// 
			textBoxSalary.Location = new Point(584, 320);
			textBoxSalary.Name = "textBoxSalary";
			textBoxSalary.Size = new Size(184, 27);
			textBoxSalary.TabIndex = 45;
			// 
			// labelSalary
			// 
			labelSalary.AutoSize = true;
			labelSalary.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelSalary.Location = new Point(472, 320);
			labelSalary.Name = "labelSalary";
			labelSalary.Size = new Size(100, 28);
			labelSalary.TabIndex = 44;
			labelSalary.Text = "Зарплата:";
			// 
			// ManageUsers
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1205, 455);
			Controls.Add(textBoxSalary);
			Controls.Add(labelSalary);
			Controls.Add(buttonEdit);
			Controls.Add(buttonExit);
			Controls.Add(comboBoxUserRoles);
			Controls.Add(dateTimePickerBirthday);
			Controls.Add(buttonFindUser);
			Controls.Add(labelUserRole);
			Controls.Add(textBoxUserCode);
			Controls.Add(labelUserCode);
			Controls.Add(textBoxPasswordConfirm);
			Controls.Add(label9);
			Controls.Add(textBoxPassword);
			Controls.Add(label8);
			Controls.Add(textBoxLogin);
			Controls.Add(label7);
			Controls.Add(textBoxPhone);
			Controls.Add(label6);
			Controls.Add(textBoxAdres);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(textBoxMiddlename);
			Controls.Add(label2);
			Controls.Add(textBoxSurname);
			Controls.Add(label1);
			Controls.Add(textBoxName);
			Controls.Add(label3);
			Controls.Add(labelTitle);
			Controls.Add(menuStripWorkMode);
			Name = "ManageUsers";
			Text = "ManageUsers";
			menuStripWorkMode.ResumeLayout(false);
			menuStripWorkMode.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private MenuStrip menuStripWorkMode;
		private ToolStripMenuItem ModeToolStripMenuItem;
		private ToolStripMenuItem AddToolStripMenuItem;
		private ToolStripMenuItem UpdateToolStripMenuItem;
		private ToolStripMenuItem DeleteToolStripMenuItem;
		private Label labelTitle;
		private TextBox textBoxName;
		private Label label3;
		private TextBox textBoxSurname;
		private Label label1;
		private TextBox textBoxMiddlename;
		private Label label2;
		private Label label4;
		private TextBox textBoxAdres;
		private Label label5;
		private TextBox textBoxPhone;
		private Label label6;
		private TextBox textBoxLogin;
		private Label label7;
		private TextBox textBoxPassword;
		private Label label8;
		private TextBox textBoxPasswordConfirm;
		private Label label9;
		private TextBox textBoxUserCode;
		private Label labelUserCode;
		private Label labelUserRole;
		private Button buttonFindUser;
		private DateTimePicker dateTimePickerBirthday;
		private ComboBox comboBoxUserRoles;
		private Button buttonExit;
		private Button buttonEdit;
		private TextBox textBoxSalary;
		private Label labelSalary;
	}
}