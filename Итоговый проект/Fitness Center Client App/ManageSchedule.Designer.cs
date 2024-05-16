namespace Fitness_Center_Client_App
{
	partial class ManageSchedule
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
			menuStripMode = new MenuStrip();
			modeToolStripMenuItem = new ToolStripMenuItem();
			addToolStripMenuItem = new ToolStripMenuItem();
			updateToolStripMenuItem = new ToolStripMenuItem();
			deleteToolStripMenuItem = new ToolStripMenuItem();
			labelTitle = new Label();
			dateTimePickerDateConduction = new DateTimePicker();
			label1 = new Label();
			label2 = new Label();
			dateTimePickerTimeStart = new DateTimePicker();
			comboBoxServiceTypes = new ComboBox();
			label3 = new Label();
			comboBoxTrainingTypes = new ComboBox();
			label4 = new Label();
			comboBoxTrainers = new ComboBox();
			label5 = new Label();
			label6 = new Label();
			dateTimePickerTimeEnd = new DateTimePicker();
			buttonExit = new Button();
			buttonEdit = new Button();
			buttonFindSchedule = new Button();
			textBoxScheduleCode = new TextBox();
			labelScheduleCode = new Label();
			menuStripMode.SuspendLayout();
			SuspendLayout();
			// 
			// menuStripMode
			// 
			menuStripMode.ImageScalingSize = new Size(20, 20);
			menuStripMode.Items.AddRange(new ToolStripItem[] { modeToolStripMenuItem });
			menuStripMode.Location = new Point(0, 0);
			menuStripMode.Name = "menuStripMode";
			menuStripMode.Size = new Size(740, 28);
			menuStripMode.TabIndex = 0;
			menuStripMode.Text = "menuStrip1";
			// 
			// modeToolStripMenuItem
			// 
			modeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addToolStripMenuItem, updateToolStripMenuItem, deleteToolStripMenuItem });
			modeToolStripMenuItem.Name = "modeToolStripMenuItem";
			modeToolStripMenuItem.Size = new Size(70, 24);
			modeToolStripMenuItem.Text = "Режим";
			// 
			// addToolStripMenuItem
			// 
			addToolStripMenuItem.Name = "addToolStripMenuItem";
			addToolStripMenuItem.Size = new Size(161, 26);
			addToolStripMenuItem.Text = "Добавить";
			addToolStripMenuItem.Click += AddToolStripMenuItem_Click;
			// 
			// updateToolStripMenuItem
			// 
			updateToolStripMenuItem.Name = "updateToolStripMenuItem";
			updateToolStripMenuItem.Size = new Size(161, 26);
			updateToolStripMenuItem.Text = "Обновить";
			updateToolStripMenuItem.Click += UpdateToolStripMenuItem_Click;
			// 
			// deleteToolStripMenuItem
			// 
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new Size(161, 26);
			deleteToolStripMenuItem.Text = "Удалить";
			deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
			// 
			// labelTitle
			// 
			labelTitle.AutoSize = true;
			labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			labelTitle.Location = new Point(128, 32);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new Size(461, 41);
			labelTitle.TabIndex = 1;
			labelTitle.Text = "Режим Добавления Расписания ";
			// 
			// dateTimePickerDateConduction
			// 
			dateTimePickerDateConduction.Location = new Point(184, 104);
			dateTimePickerDateConduction.Name = "dateTimePickerDateConduction";
			dateTimePickerDateConduction.Size = new Size(184, 27);
			dateTimePickerDateConduction.TabIndex = 2;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(8, 104);
			label1.Name = "label1";
			label1.Size = new Size(175, 28);
			label1.TabIndex = 3;
			label1.Text = "Дата проведения:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(8, 152);
			label2.Name = "label2";
			label2.Size = new Size(189, 28);
			label2.TabIndex = 5;
			label2.Text = "Дата проведения: с";
			// 
			// dateTimePickerTimeStart
			// 
			dateTimePickerTimeStart.Format = DateTimePickerFormat.Time;
			dateTimePickerTimeStart.Location = new Point(200, 152);
			dateTimePickerTimeStart.Name = "dateTimePickerTimeStart";
			dateTimePickerTimeStart.Size = new Size(104, 27);
			dateTimePickerTimeStart.TabIndex = 4;
			// 
			// comboBoxServiceTypes
			// 
			comboBoxServiceTypes.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxServiceTypes.FormattingEnabled = true;
			comboBoxServiceTypes.Location = new Point(184, 256);
			comboBoxServiceTypes.Name = "comboBoxServiceTypes";
			comboBoxServiceTypes.Size = new Size(151, 28);
			comboBoxServiceTypes.TabIndex = 10;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(64, 256);
			label3.Name = "label3";
			label3.Size = new Size(120, 28);
			label3.TabIndex = 9;
			label3.Text = "Тип услуги: ";
			// 
			// comboBoxTrainingTypes
			// 
			comboBoxTrainingTypes.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxTrainingTypes.FormattingEnabled = true;
			comboBoxTrainingTypes.Location = new Point(184, 208);
			comboBoxTrainingTypes.Name = "comboBoxTrainingTypes";
			comboBoxTrainingTypes.Size = new Size(151, 28);
			comboBoxTrainingTypes.TabIndex = 8;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(16, 208);
			label4.Name = "label4";
			label4.Size = new Size(171, 28);
			label4.TabIndex = 7;
			label4.Text = "Тип тренировки: ";
			// 
			// comboBoxTrainers
			// 
			comboBoxTrainers.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxTrainers.FormattingEnabled = true;
			comboBoxTrainers.Location = new Point(184, 304);
			comboBoxTrainers.Name = "comboBoxTrainers";
			comboBoxTrainers.Size = new Size(151, 28);
			comboBoxTrainers.TabIndex = 12;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label5.Location = new Point(96, 304);
			label5.Name = "label5";
			label5.Size = new Size(87, 28);
			label5.TabIndex = 11;
			label5.Text = "Тренер: ";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label6.Location = new Point(312, 152);
			label6.Name = "label6";
			label6.Size = new Size(35, 28);
			label6.TabIndex = 13;
			label6.Text = "до";
			// 
			// dateTimePickerTimeEnd
			// 
			dateTimePickerTimeEnd.Format = DateTimePickerFormat.Time;
			dateTimePickerTimeEnd.Location = new Point(352, 152);
			dateTimePickerTimeEnd.Name = "dateTimePickerTimeEnd";
			dateTimePickerTimeEnd.Size = new Size(104, 27);
			dateTimePickerTimeEnd.TabIndex = 14;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(16, 352);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(184, 56);
			buttonExit.TabIndex = 15;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// buttonEdit
			// 
			buttonEdit.Location = new Point(536, 352);
			buttonEdit.Name = "buttonEdit";
			buttonEdit.Size = new Size(184, 56);
			buttonEdit.TabIndex = 16;
			buttonEdit.Text = "Выполнить";
			buttonEdit.UseVisualStyleBackColor = true;
			buttonEdit.Click += ButtonEdit_Click;
			// 
			// buttonFindSchedule
			// 
			buttonFindSchedule.Location = new Point(592, 144);
			buttonFindSchedule.Name = "buttonFindSchedule";
			buttonFindSchedule.Size = new Size(128, 32);
			buttonFindSchedule.TabIndex = 21;
			buttonFindSchedule.Text = "Найти";
			buttonFindSchedule.UseVisualStyleBackColor = true;
			buttonFindSchedule.Click += ButtonFindSchedule_Click;
			// 
			// textBoxScheduleCode
			// 
			textBoxScheduleCode.Location = new Point(640, 104);
			textBoxScheduleCode.Name = "textBoxScheduleCode";
			textBoxScheduleCode.Size = new Size(80, 27);
			textBoxScheduleCode.TabIndex = 20;
			textBoxScheduleCode.TextChanged += TextBoxScheduleCode_TextChanged;
			// 
			// labelScheduleCode
			// 
			labelScheduleCode.AutoSize = true;
			labelScheduleCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelScheduleCode.Location = new Point(472, 104);
			labelScheduleCode.Name = "labelScheduleCode";
			labelScheduleCode.Size = new Size(164, 28);
			labelScheduleCode.TabIndex = 19;
			labelScheduleCode.Text = "Код расписания:";
			// 
			// ManageSchedule
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(740, 423);
			Controls.Add(buttonFindSchedule);
			Controls.Add(textBoxScheduleCode);
			Controls.Add(labelScheduleCode);
			Controls.Add(buttonEdit);
			Controls.Add(buttonExit);
			Controls.Add(dateTimePickerTimeEnd);
			Controls.Add(label6);
			Controls.Add(comboBoxTrainers);
			Controls.Add(label5);
			Controls.Add(comboBoxServiceTypes);
			Controls.Add(label3);
			Controls.Add(comboBoxTrainingTypes);
			Controls.Add(label4);
			Controls.Add(label2);
			Controls.Add(dateTimePickerTimeStart);
			Controls.Add(label1);
			Controls.Add(dateTimePickerDateConduction);
			Controls.Add(labelTitle);
			Controls.Add(menuStripMode);
			MainMenuStrip = menuStripMode;
			Name = "ManageSchedule";
			Text = "Фитнес центр - Управление расписанием";
			menuStripMode.ResumeLayout(false);
			menuStripMode.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private MenuStrip menuStripMode;
		private ToolStripMenuItem modeToolStripMenuItem;
		private ToolStripMenuItem addToolStripMenuItem;
		private ToolStripMenuItem updateToolStripMenuItem;
		private Label labelTitle;
		private DateTimePicker dateTimePickerDateConduction;
		private Label label1;
		private Label label2;
		private DateTimePicker dateTimePickerTimeStart;
		private ComboBox comboBoxServiceTypes;
		private Label label3;
		private ComboBox comboBoxTrainingTypes;
		private Label label4;
		private ComboBox comboBoxTrainers;
		private Label label5;
		private Label label6;
		private DateTimePicker dateTimePickerTimeEnd;
		private ToolStripMenuItem deleteToolStripMenuItem;
		private Button buttonExit;
		private Button buttonEdit;
		private Button buttonFindSchedule;
		private TextBox textBoxScheduleCode;
		private Label labelScheduleCode;
	}
}