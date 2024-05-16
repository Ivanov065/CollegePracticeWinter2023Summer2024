namespace Fitness_Center_Client_App
{
	partial class ClientMenu
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
			buttonShowClientAcc = new Button();
			buttonShowSchedule = new Button();
			buttonShowSeasonTickets = new Button();
			buttonExit = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(80, 8);
			label1.Name = "label1";
			label1.Size = new Size(328, 46);
			label1.TabIndex = 0;
			label1.Text = "Добро пожаловать ";
			// 
			// buttonShowClientAcc
			// 
			buttonShowClientAcc.Location = new Point(128, 72);
			buttonShowClientAcc.Name = "buttonShowClientAcc";
			buttonShowClientAcc.Size = new Size(208, 48);
			buttonShowClientAcc.TabIndex = 1;
			buttonShowClientAcc.Text = "Посмотреть аккаунт";
			buttonShowClientAcc.UseVisualStyleBackColor = true;
			buttonShowClientAcc.Click += ButtonShowClientAcc_Click;
			// 
			// buttonShowSchedule
			// 
			buttonShowSchedule.Location = new Point(128, 152);
			buttonShowSchedule.Name = "buttonShowSchedule";
			buttonShowSchedule.Size = new Size(208, 48);
			buttonShowSchedule.TabIndex = 2;
			buttonShowSchedule.Text = "Посмотреть расписание";
			buttonShowSchedule.UseVisualStyleBackColor = true;
			buttonShowSchedule.Click += ButtonShowSchedule_Click;
			// 
			// buttonShowSeasonTickets
			// 
			buttonShowSeasonTickets.Location = new Point(128, 232);
			buttonShowSeasonTickets.Name = "buttonShowSeasonTickets";
			buttonShowSeasonTickets.Size = new Size(208, 48);
			buttonShowSeasonTickets.TabIndex = 3;
			buttonShowSeasonTickets.Text = "Посмотреть Абонементы";
			buttonShowSeasonTickets.UseVisualStyleBackColor = true;
			buttonShowSeasonTickets.Click += ButtonShowSeasonTickets_Click;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(128, 312);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(208, 48);
			buttonExit.TabIndex = 4;
			buttonExit.Text = "Выход";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// ClientMenu
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(496, 401);
			Controls.Add(buttonExit);
			Controls.Add(buttonShowSeasonTickets);
			Controls.Add(buttonShowSchedule);
			Controls.Add(buttonShowClientAcc);
			Controls.Add(label1);
			Name = "ClientMenu";
			Text = "Фитнес клуб - Меню клиента";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Button buttonShowClientAcc;
		private Button buttonShowSchedule;
		private Button buttonShowSeasonTickets;
		private Button buttonExit;
	}
}