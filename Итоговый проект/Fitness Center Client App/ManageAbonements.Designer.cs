namespace Fitness_Center_Client_App
{
	partial class ManageAbonements
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
			label1 = new Label();
			comboBoxTrainingTypes = new ComboBox();
			comboBoxServiceTypes = new ComboBox();
			label2 = new Label();
			label3 = new Label();
			textBoxPrice = new TextBox();
			textBoxVisitsAmount = new TextBox();
			label4 = new Label();
			textBoxTicketDuration = new TextBox();
			label5 = new Label();
			buttonExit = new Button();
			buttonEdit = new Button();
			labelTitle = new Label();
			textBoxTicketCode = new TextBox();
			labelTicketCode = new Label();
			buttonFindTicket = new Button();
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
			menuStripWorkMode.Size = new Size(800, 31);
			menuStripWorkMode.TabIndex = 1;
			menuStripWorkMode.Text = "menuStrip1";
			// 
			// ModeToolStripMenuItem
			// 
			ModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AddToolStripMenuItem, UpdateToolStripMenuItem });
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
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(16, 88);
			label1.Name = "label1";
			label1.Size = new Size(171, 28);
			label1.TabIndex = 3;
			label1.Text = "Тип тренировки: ";
			// 
			// comboBoxTrainingTypes
			// 
			comboBoxTrainingTypes.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxTrainingTypes.FormattingEnabled = true;
			comboBoxTrainingTypes.Location = new Point(184, 88);
			comboBoxTrainingTypes.Name = "comboBoxTrainingTypes";
			comboBoxTrainingTypes.Size = new Size(151, 28);
			comboBoxTrainingTypes.TabIndex = 4;
			// 
			// comboBoxServiceTypes
			// 
			comboBoxServiceTypes.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxServiceTypes.FormattingEnabled = true;
			comboBoxServiceTypes.Location = new Point(184, 152);
			comboBoxServiceTypes.Name = "comboBoxServiceTypes";
			comboBoxServiceTypes.Size = new Size(151, 28);
			comboBoxServiceTypes.TabIndex = 6;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(64, 152);
			label2.Name = "label2";
			label2.Size = new Size(120, 28);
			label2.TabIndex = 5;
			label2.Text = "Тип услуги: ";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(120, 216);
			label3.Name = "label3";
			label3.Size = new Size(68, 28);
			label3.TabIndex = 7;
			label3.Text = "Цена: ";
			// 
			// textBoxPrice
			// 
			textBoxPrice.Location = new Point(184, 216);
			textBoxPrice.Name = "textBoxPrice";
			textBoxPrice.Size = new Size(152, 27);
			textBoxPrice.TabIndex = 8;
			// 
			// textBoxVisitsAmount
			// 
			textBoxVisitsAmount.Location = new Point(560, 152);
			textBoxVisitsAmount.Name = "textBoxVisitsAmount";
			textBoxVisitsAmount.Size = new Size(152, 27);
			textBoxVisitsAmount.TabIndex = 10;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(360, 152);
			label4.Name = "label4";
			label4.Size = new Size(197, 28);
			label4.TabIndex = 9;
			label4.Text = "Кол-во посещений: ";
			// 
			// textBoxTicketDuration
			// 
			textBoxTicketDuration.Location = new Point(560, 216);
			textBoxTicketDuration.Name = "textBoxTicketDuration";
			textBoxTicketDuration.Size = new Size(152, 27);
			textBoxTicketDuration.TabIndex = 12;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label5.Location = new Point(352, 216);
			label5.Name = "label5";
			label5.Size = new Size(210, 28);
			label5.TabIndex = 11;
			label5.Text = "Длительность (дней): ";
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(8, 264);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(160, 48);
			buttonExit.TabIndex = 13;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// buttonEdit
			// 
			buttonEdit.Location = new Point(632, 264);
			buttonEdit.Name = "buttonEdit";
			buttonEdit.Size = new Size(160, 48);
			buttonEdit.TabIndex = 14;
			buttonEdit.Text = "Выполнить";
			buttonEdit.UseVisualStyleBackColor = true;
			buttonEdit.Click += ButtonEdit_Click;
			// 
			// labelTitle
			// 
			labelTitle.AutoSize = true;
			labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			labelTitle.Location = new Point(160, 16);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new Size(482, 41);
			labelTitle.TabIndex = 15;
			labelTitle.Text = "Режим Добавления Абонементов";
			// 
			// textBoxTicketCode
			// 
			textBoxTicketCode.Location = new Point(560, 88);
			textBoxTicketCode.Name = "textBoxTicketCode";
			textBoxTicketCode.Size = new Size(80, 27);
			textBoxTicketCode.TabIndex = 17;
			textBoxTicketCode.TextChanged += TextBoxTicketCode_TextChanged;
			// 
			// labelTicketCode
			// 
			labelTicketCode.AutoSize = true;
			labelTicketCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelTicketCode.Location = new Point(384, 88);
			labelTicketCode.Name = "labelTicketCode";
			labelTicketCode.Size = new Size(171, 28);
			labelTicketCode.TabIndex = 16;
			labelTicketCode.Text = "Код абонемента: ";
			// 
			// buttonFindTicket
			// 
			buttonFindTicket.Location = new Point(648, 88);
			buttonFindTicket.Name = "buttonFindTicket";
			buttonFindTicket.Size = new Size(128, 29);
			buttonFindTicket.TabIndex = 18;
			buttonFindTicket.Text = "Найти";
			buttonFindTicket.UseVisualStyleBackColor = true;
			buttonFindTicket.Click += ButtonFindTicket_Click;
			// 
			// ManageAbonements
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 327);
			Controls.Add(buttonFindTicket);
			Controls.Add(textBoxTicketCode);
			Controls.Add(labelTicketCode);
			Controls.Add(labelTitle);
			Controls.Add(buttonEdit);
			Controls.Add(buttonExit);
			Controls.Add(textBoxTicketDuration);
			Controls.Add(label5);
			Controls.Add(textBoxVisitsAmount);
			Controls.Add(label4);
			Controls.Add(textBoxPrice);
			Controls.Add(label3);
			Controls.Add(comboBoxServiceTypes);
			Controls.Add(label2);
			Controls.Add(comboBoxTrainingTypes);
			Controls.Add(label1);
			Controls.Add(menuStripWorkMode);
			MainMenuStrip = menuStripWorkMode;
			Name = "ManageAbonements";
			Text = "Фитнес клуб - Управление абонементами";
			menuStripWorkMode.ResumeLayout(false);
			menuStripWorkMode.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private MenuStrip menuStripWorkMode;
		private ToolStripMenuItem ModeToolStripMenuItem;
		private Label label1;
		private ComboBox comboBoxTrainingTypes;
		private ComboBox comboBoxServiceTypes;
		private Label label2;
		private Label label3;
		private TextBox textBoxPrice;
		private TextBox textBoxVisitsAmount;
		private Label label4;
		private TextBox textBoxTicketDuration;
		private Label label5;
		private Button buttonExit;
		private Button buttonEdit;
		private ToolStripMenuItem AddToolStripMenuItem;
		private ToolStripMenuItem UpdateToolStripMenuItem;
		private Label labelTitle;
		private TextBox textBoxTicketCode;
		private Label labelTicketCode;
		private Button buttonFindTicket;
	}
}