namespace Fitness_Center_Client_App
{
	partial class ManagerMenu
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
			buttonEmpAcc = new Button();
			buttonShowSchedule = new Button();
			buttonShowClients = new Button();
			buttonShowTickets = new Button();
			buttonManageSchedule = new Button();
			buttonShowTrainers = new Button();
			buttonManageClients = new Button();
			buttonManageTickets = new Button();
			label2 = new Label();
			label3 = new Label();
			buttonExit = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(136, 8);
			label1.Name = "label1";
			label1.Size = new Size(319, 46);
			label1.TabIndex = 0;
			label1.Text = "Добро пожаловать";
			// 
			// buttonEmpAcc
			// 
			buttonEmpAcc.Location = new Point(32, 112);
			buttonEmpAcc.Name = "buttonEmpAcc";
			buttonEmpAcc.Size = new Size(184, 48);
			buttonEmpAcc.TabIndex = 1;
			buttonEmpAcc.Text = "Ваш аккаунт";
			buttonEmpAcc.UseVisualStyleBackColor = true;
			buttonEmpAcc.Click += buttonEmpAcc_Click;
			// 
			// buttonShowSchedule
			// 
			buttonShowSchedule.Location = new Point(32, 328);
			buttonShowSchedule.Name = "buttonShowSchedule";
			buttonShowSchedule.Size = new Size(184, 48);
			buttonShowSchedule.TabIndex = 2;
			buttonShowSchedule.Text = "Расписание";
			buttonShowSchedule.UseVisualStyleBackColor = true;
			buttonShowSchedule.Click += ButtonShowSchedule_Click;
			// 
			// buttonShowClients
			// 
			buttonShowClients.Location = new Point(32, 400);
			buttonShowClients.Name = "buttonShowClients";
			buttonShowClients.Size = new Size(184, 48);
			buttonShowClients.TabIndex = 4;
			buttonShowClients.Text = "Клиенты";
			buttonShowClients.UseVisualStyleBackColor = true;
			buttonShowClients.Click += ButtonShowClients_Click;
			// 
			// buttonShowTickets
			// 
			buttonShowTickets.Location = new Point(32, 184);
			buttonShowTickets.Name = "buttonShowTickets";
			buttonShowTickets.Size = new Size(184, 48);
			buttonShowTickets.TabIndex = 3;
			buttonShowTickets.Text = "Абонементы";
			buttonShowTickets.UseVisualStyleBackColor = true;
			buttonShowTickets.Click += ButtonShowTickets_Click;
			// 
			// buttonManageSchedule
			// 
			buttonManageSchedule.Location = new Point(320, 184);
			buttonManageSchedule.Name = "buttonManageSchedule";
			buttonManageSchedule.Size = new Size(184, 48);
			buttonManageSchedule.TabIndex = 6;
			buttonManageSchedule.Text = "Расписание";
			buttonManageSchedule.UseVisualStyleBackColor = true;
			buttonManageSchedule.Click += ButtonManageSchedule_Click;
			// 
			// buttonShowTrainers
			// 
			buttonShowTrainers.Location = new Point(32, 256);
			buttonShowTrainers.Name = "buttonShowTrainers";
			buttonShowTrainers.Size = new Size(184, 48);
			buttonShowTrainers.TabIndex = 5;
			buttonShowTrainers.Text = "Тренеры";
			buttonShowTrainers.UseVisualStyleBackColor = true;
			buttonShowTrainers.Click += ButtonShowTrainers_Click;
			// 
			// buttonManageClients
			// 
			buttonManageClients.Location = new Point(320, 256);
			buttonManageClients.Name = "buttonManageClients";
			buttonManageClients.Size = new Size(184, 48);
			buttonManageClients.TabIndex = 8;
			buttonManageClients.Text = "Клиентов";
			buttonManageClients.UseVisualStyleBackColor = true;
			buttonManageClients.Click += ButtonManageClients_Click;
			// 
			// buttonManageTickets
			// 
			buttonManageTickets.Location = new Point(320, 112);
			buttonManageTickets.Name = "buttonManageTickets";
			buttonManageTickets.Size = new Size(184, 48);
			buttonManageTickets.TabIndex = 7;
			buttonManageTickets.Text = "Абонементы";
			buttonManageTickets.UseVisualStyleBackColor = true;
			buttonManageTickets.Click += ButtonManageTickets_Click;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(32, 64);
			label2.Name = "label2";
			label2.Size = new Size(146, 32);
			label2.TabIndex = 9;
			label2.Text = "Посмотреть";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(248, 64);
			label3.Name = "label3";
			label3.Size = new Size(327, 32);
			label3.TabIndex = 10;
			label3.Text = "Удалить/обновить/добавить";
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(408, 408);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(184, 48);
			buttonExit.TabIndex = 11;
			buttonExit.Text = "Выйти";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// ManagerMenu
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(600, 465);
			Controls.Add(buttonExit);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(buttonManageClients);
			Controls.Add(buttonManageTickets);
			Controls.Add(buttonManageSchedule);
			Controls.Add(buttonShowTrainers);
			Controls.Add(buttonShowClients);
			Controls.Add(buttonShowTickets);
			Controls.Add(buttonShowSchedule);
			Controls.Add(buttonEmpAcc);
			Controls.Add(label1);
			Name = "ManagerMenu";
			Text = "ManagerMenu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Button buttonEmpAcc;
		private Button buttonShowSchedule;
		private Button buttonShowClients;
		private Button buttonShowTickets;
		private Button buttonManageSchedule;
		private Button buttonShowTrainers;
		private Button buttonManageClients;
		private Button buttonManageTickets;
		private Label label2;
		private Label label3;
		private Button buttonExit;
	}
}