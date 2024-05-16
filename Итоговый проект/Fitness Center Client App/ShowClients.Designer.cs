namespace Fitness_Center_Client_App
{
	partial class ShowClients
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
			dataGridViewClients = new DataGridView();
			buttonExit = new Button();
			textBoxClientCode = new TextBox();
			label2 = new Label();
			buttonFindTicket = new Button();
			labelTicketId = new Label();
			labelTicketTemplateCode = new Label();
			labelClientCode = new Label();
			labelTimeActive = new Label();
			labelLastVisits = new Label();
			labelTrainingType = new Label();
			labelServiceType = new Label();
			((System.ComponentModel.ISupportInitialize)dataGridViewClients).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(200, 8);
			label1.Name = "label1";
			label1.Size = new Size(365, 46);
			label1.TabIndex = 0;
			label1.Text = "Список всех клиентов";
			// 
			// dataGridViewClients
			// 
			dataGridViewClients.AllowUserToAddRows = false;
			dataGridViewClients.AllowUserToDeleteRows = false;
			dataGridViewClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewClients.Location = new Point(16, 64);
			dataGridViewClients.Name = "dataGridViewClients";
			dataGridViewClients.ReadOnly = true;
			dataGridViewClients.RowHeadersWidth = 51;
			dataGridViewClients.RowTemplate.Height = 29;
			dataGridViewClients.Size = new Size(792, 368);
			dataGridViewClients.TabIndex = 1;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(600, 664);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(192, 53);
			buttonExit.TabIndex = 2;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// textBoxClientCode
			// 
			textBoxClientCode.Location = new Point(552, 448);
			textBoxClientCode.Name = "textBoxClientCode";
			textBoxClientCode.Size = new Size(144, 27);
			textBoxClientCode.TabIndex = 3;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(16, 448);
			label2.Name = "label2";
			label2.Size = new Size(529, 28);
			label2.TabIndex = 4;
			label2.Text = "Введите ID клиента, чтобы увидеть активный абонемент";
			// 
			// buttonFindTicket
			// 
			buttonFindTicket.Location = new Point(704, 448);
			buttonFindTicket.Name = "buttonFindTicket";
			buttonFindTicket.Size = new Size(112, 29);
			buttonFindTicket.TabIndex = 5;
			buttonFindTicket.Text = "Найти";
			buttonFindTicket.UseVisualStyleBackColor = true;
			buttonFindTicket.Click += ButtonFindTicket_Click;
			// 
			// labelTicketId
			// 
			labelTicketId.AutoSize = true;
			labelTicketId.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			labelTicketId.Location = new Point(16, 488);
			labelTicketId.Name = "labelTicketId";
			labelTicketId.Size = new Size(415, 41);
			labelTicketId.TabIndex = 6;
			labelTicketId.Text = "Активный абонемент номер ";
			// 
			// labelTicketTemplateCode
			// 
			labelTicketTemplateCode.AutoSize = true;
			labelTicketTemplateCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelTicketTemplateCode.Location = new Point(16, 544);
			labelTicketTemplateCode.Name = "labelTicketTemplateCode";
			labelTicketTemplateCode.Size = new Size(259, 28);
			labelTicketTemplateCode.TabIndex = 7;
			labelTicketTemplateCode.Text = "Код шаблона абонемента: ";
			// 
			// labelClientCode
			// 
			labelClientCode.AutoSize = true;
			labelClientCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelClientCode.Location = new Point(16, 592);
			labelClientCode.Name = "labelClientCode";
			labelClientCode.Size = new Size(134, 28);
			labelClientCode.TabIndex = 8;
			labelClientCode.Text = "Код клиента: ";
			// 
			// labelTimeActive
			// 
			labelTimeActive.AutoSize = true;
			labelTimeActive.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelTimeActive.Location = new Point(16, 640);
			labelTimeActive.Name = "labelTimeActive";
			labelTimeActive.Size = new Size(151, 28);
			labelTimeActive.TabIndex = 9;
			labelTimeActive.Text = "Время работы: ";
			// 
			// labelLastVisits
			// 
			labelLastVisits.AutoSize = true;
			labelLastVisits.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelLastVisits.Location = new Point(16, 688);
			labelLastVisits.Name = "labelLastVisits";
			labelLastVisits.Size = new Size(215, 28);
			labelLastVisits.TabIndex = 10;
			labelLastVisits.Text = "Осталось посещений: ";
			// 
			// labelTrainingType
			// 
			labelTrainingType.AutoSize = true;
			labelTrainingType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelTrainingType.Location = new Point(408, 544);
			labelTrainingType.Name = "labelTrainingType";
			labelTrainingType.Size = new Size(171, 28);
			labelTrainingType.TabIndex = 11;
			labelTrainingType.Text = "Тип тренировки: ";
			// 
			// labelServiceType
			// 
			labelServiceType.AutoSize = true;
			labelServiceType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelServiceType.Location = new Point(408, 592);
			labelServiceType.Name = "labelServiceType";
			labelServiceType.Size = new Size(120, 28);
			labelServiceType.TabIndex = 12;
			labelServiceType.Text = "Тип услуги: ";
			// 
			// ShowClients
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(829, 732);
			Controls.Add(labelServiceType);
			Controls.Add(labelTrainingType);
			Controls.Add(labelLastVisits);
			Controls.Add(labelTimeActive);
			Controls.Add(labelClientCode);
			Controls.Add(labelTicketTemplateCode);
			Controls.Add(labelTicketId);
			Controls.Add(buttonFindTicket);
			Controls.Add(label2);
			Controls.Add(textBoxClientCode);
			Controls.Add(buttonExit);
			Controls.Add(dataGridViewClients);
			Controls.Add(label1);
			Name = "ShowClients";
			Text = "ShowClients";
			((System.ComponentModel.ISupportInitialize)dataGridViewClients).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private DataGridView dataGridViewClients;
		private Button buttonExit;
		private TextBox textBoxClientCode;
		private Label label2;
		private Button buttonFindTicket;
		private Label labelTicketId;
		private Label labelTicketTemplateCode;
		private Label labelClientCode;
		private Label labelTimeActive;
		private Label labelLastVisits;
		private Label labelTrainingType;
		private Label labelServiceType;
	}
}