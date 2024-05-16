namespace Fitness_Center_Client_App
{
	partial class TrainerMenu
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
			buttonShowAcc = new Button();
			buttonSchedule = new Button();
			buttonExit = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(72, 8);
			label1.Name = "label1";
			label1.Size = new Size(319, 46);
			label1.TabIndex = 0;
			label1.Text = "Добро пожаловать";
			// 
			// buttonShowAcc
			// 
			buttonShowAcc.Location = new Point(128, 72);
			buttonShowAcc.Name = "buttonShowAcc";
			buttonShowAcc.Size = new Size(200, 48);
			buttonShowAcc.TabIndex = 1;
			buttonShowAcc.Text = "Мой акаунт";
			buttonShowAcc.UseVisualStyleBackColor = true;
			buttonShowAcc.Click += ButtonShowAcc_Click;
			// 
			// buttonSchedule
			// 
			buttonSchedule.Location = new Point(128, 152);
			buttonSchedule.Name = "buttonSchedule";
			buttonSchedule.Size = new Size(200, 48);
			buttonSchedule.TabIndex = 2;
			buttonSchedule.Text = "Посмотреть расписание";
			buttonSchedule.UseVisualStyleBackColor = true;
			buttonSchedule.Click += ButtonSchedule_Click;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(128, 232);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(200, 48);
			buttonExit.TabIndex = 3;
			buttonExit.Text = "Выход";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// TrainerMenu
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(476, 336);
			Controls.Add(buttonExit);
			Controls.Add(buttonSchedule);
			Controls.Add(buttonShowAcc);
			Controls.Add(label1);
			Name = "TrainerMenu";
			Text = "TrainerMenu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Button buttonShowAcc;
		private Button buttonSchedule;
		private Button buttonExit;
	}
}