namespace Fitness_Center_Client_App
{
	partial class EmployeeAcc
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
			label1 = new Label();
			labelFIO = new Label();
			labelBirthday = new Label();
			labelAdres = new Label();
			labelPhone = new Label();
			labelRole = new Label();
			labelSalary = new Label();
			buttonExit = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(120, 16);
			label1.Name = "label1";
			label1.Size = new Size(215, 46);
			label1.TabIndex = 0;
			label1.Text = "Ваш Аккаунт";
			// 
			// labelFIO
			// 
			labelFIO.AutoSize = true;
			labelFIO.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelFIO.Location = new Point(8, 72);
			labelFIO.Name = "labelFIO";
			labelFIO.Size = new Size(66, 28);
			labelFIO.TabIndex = 1;
			labelFIO.Text = "ФИО: ";
			// 
			// labelBirthday
			// 
			labelBirthday.AutoSize = true;
			labelBirthday.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelBirthday.Location = new Point(8, 112);
			labelBirthday.Name = "labelBirthday";
			labelBirthday.Size = new Size(161, 28);
			labelBirthday.TabIndex = 2;
			labelBirthday.Text = "Дата Рождения: ";
			// 
			// labelAdres
			// 
			labelAdres.AutoSize = true;
			labelAdres.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelAdres.Location = new Point(8, 152);
			labelAdres.Name = "labelAdres";
			labelAdres.Size = new Size(76, 28);
			labelAdres.TabIndex = 3;
			labelAdres.Text = "Адрес: ";
			// 
			// labelPhone
			// 
			labelPhone.AutoSize = true;
			labelPhone.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelPhone.Location = new Point(8, 192);
			labelPhone.Name = "labelPhone";
			labelPhone.Size = new Size(100, 28);
			labelPhone.TabIndex = 4;
			labelPhone.Text = "Телефон: ";
			// 
			// labelRole
			// 
			labelRole.AutoSize = true;
			labelRole.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelRole.Location = new Point(8, 232);
			labelRole.Name = "labelRole";
			labelRole.Size = new Size(173, 28);
			labelRole.TabIndex = 5;
			labelRole.Text = "Ваша должность: ";
			// 
			// labelSalary
			// 
			labelSalary.AutoSize = true;
			labelSalary.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelSalary.Location = new Point(8, 272);
			labelSalary.Name = "labelSalary";
			labelSalary.Size = new Size(78, 28);
			labelSalary.TabIndex = 6;
			labelSalary.Text = "Оклад: ";
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(136, 336);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(168, 48);
			buttonExit.TabIndex = 7;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// EmployeeAcc
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(452, 401);
			Controls.Add(buttonExit);
			Controls.Add(labelSalary);
			Controls.Add(labelRole);
			Controls.Add(labelPhone);
			Controls.Add(labelAdres);
			Controls.Add(labelBirthday);
			Controls.Add(labelFIO);
			Controls.Add(label1);
			Name = "EmployeeAcc";
			Text = "EmployeeAcc";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label labelFIO;
		private Label labelBirthday;
		private Label labelAdres;
		private Label labelPhone;
		private Label labelRole;
		private Label labelSalary;
		private Button buttonExit;
	}
}