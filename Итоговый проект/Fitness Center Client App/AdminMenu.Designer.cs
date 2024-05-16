namespace Fitness_Center_Client_App
{
	partial class AdminMenu
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
			buttonExit = new Button();
			label3 = new Label();
			label2 = new Label();
			buttonManageClients = new Button();
			buttonManageTickets = new Button();
			buttonManageSchedule = new Button();
			buttonShowTrainers = new Button();
			buttonShowClients = new Button();
			buttonShowTickets = new Button();
			buttonShowSchedule = new Button();
			buttonEmpAcc = new Button();
			label1 = new Label();
			SuspendLayout();
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(384, 392);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(184, 48);
			buttonExit.TabIndex = 23;
			buttonExit.Text = "Выйти";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(232, 56);
			label3.Name = "label3";
			label3.Size = new Size(327, 32);
			label3.TabIndex = 22;
			label3.Text = "Удалить/обновить/добавить";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(16, 56);
			label2.Name = "label2";
			label2.Size = new Size(146, 32);
			label2.TabIndex = 21;
			label2.Text = "Посмотреть";
			// 
			// buttonManageClients
			// 
			buttonManageClients.Location = new Point(304, 248);
			buttonManageClients.Name = "buttonManageClients";
			buttonManageClients.Size = new Size(184, 48);
			buttonManageClients.TabIndex = 20;
			buttonManageClients.Text = "Клиентов";
			buttonManageClients.UseVisualStyleBackColor = true;
			buttonManageClients.Click += ButtonManageClients_Click;
			// 
			// buttonManageTickets
			// 
			buttonManageTickets.Location = new Point(304, 104);
			buttonManageTickets.Name = "buttonManageTickets";
			buttonManageTickets.Size = new Size(184, 48);
			buttonManageTickets.TabIndex = 19;
			buttonManageTickets.Text = "Абонементы";
			buttonManageTickets.UseVisualStyleBackColor = true;
			buttonManageTickets.Click += ButtonManageTickets_Click;
			// 
			// buttonManageSchedule
			// 
			buttonManageSchedule.Location = new Point(304, 176);
			buttonManageSchedule.Name = "buttonManageSchedule";
			buttonManageSchedule.Size = new Size(184, 48);
			buttonManageSchedule.TabIndex = 18;
			buttonManageSchedule.Text = "Расписание";
			buttonManageSchedule.UseVisualStyleBackColor = true;
			buttonManageSchedule.Click += ButtonManageSchedule_Click;
			// 
			// buttonShowTrainers
			// 
			buttonShowTrainers.Location = new Point(16, 248);
			buttonShowTrainers.Name = "buttonShowTrainers";
			buttonShowTrainers.Size = new Size(184, 48);
			buttonShowTrainers.TabIndex = 17;
			buttonShowTrainers.Text = "Тренеры";
			buttonShowTrainers.UseVisualStyleBackColor = true;
			buttonShowTrainers.Click += ButtonShowTrainers_Click;
			// 
			// buttonShowClients
			// 
			buttonShowClients.Location = new Point(16, 392);
			buttonShowClients.Name = "buttonShowClients";
			buttonShowClients.Size = new Size(184, 48);
			buttonShowClients.TabIndex = 16;
			buttonShowClients.Text = "Клиенты";
			buttonShowClients.UseVisualStyleBackColor = true;
			buttonShowClients.Click += ButtonShowClients_Click;
			// 
			// buttonShowTickets
			// 
			buttonShowTickets.Location = new Point(16, 176);
			buttonShowTickets.Name = "buttonShowTickets";
			buttonShowTickets.Size = new Size(184, 48);
			buttonShowTickets.TabIndex = 15;
			buttonShowTickets.Text = "Абонементы";
			buttonShowTickets.UseVisualStyleBackColor = true;
			buttonShowTickets.Click += ButtonShowTickets_Click;
			// 
			// buttonShowSchedule
			// 
			buttonShowSchedule.Location = new Point(16, 320);
			buttonShowSchedule.Name = "buttonShowSchedule";
			buttonShowSchedule.Size = new Size(184, 48);
			buttonShowSchedule.TabIndex = 14;
			buttonShowSchedule.Text = "Расписание";
			buttonShowSchedule.UseVisualStyleBackColor = true;
			buttonShowSchedule.Click += ButtonShowSchedule_Click;
			// 
			// buttonEmpAcc
			// 
			buttonEmpAcc.Location = new Point(16, 104);
			buttonEmpAcc.Name = "buttonEmpAcc";
			buttonEmpAcc.Size = new Size(184, 48);
			buttonEmpAcc.TabIndex = 13;
			buttonEmpAcc.Text = "Ваш аккаунт";
			buttonEmpAcc.UseVisualStyleBackColor = true;
			buttonEmpAcc.Click += ButtonEmpAcc_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(120, 8);
			label1.Name = "label1";
			label1.Size = new Size(319, 46);
			label1.TabIndex = 12;
			label1.Text = "Добро пожаловать";
			// 
			// AdminMenu
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(581, 459);
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
			Name = "AdminMenu";
			Text = "AdminMenu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button buttonExit;
		private Label label3;
		private Label label2;
		private Button buttonManageClients;
		private Button buttonManageTickets;
		private Button buttonManageSchedule;
		private Button buttonShowTrainers;
		private Button buttonShowClients;
		private Button buttonShowTickets;
		private Button buttonShowSchedule;
		private Button buttonEmpAcc;
		private Label label1;
	}
}