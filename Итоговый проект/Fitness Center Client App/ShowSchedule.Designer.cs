namespace Fitness_Center_Client_App
{
	partial class ShowSchedule
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
			dataGridViewSchedule = new DataGridView();
			buttonExit = new Button();
			label1 = new Label();
			dateTimePickerScheduleDate = new DateTimePicker();
			buttonRefreshTable = new Button();
			buttonTrainerSchedule = new Button();
			((System.ComponentModel.ISupportInitialize)dataGridViewSchedule).BeginInit();
			SuspendLayout();
			// 
			// dataGridViewSchedule
			// 
			dataGridViewSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewSchedule.Location = new Point(16, 56);
			dataGridViewSchedule.Name = "dataGridViewSchedule";
			dataGridViewSchedule.ReadOnly = true;
			dataGridViewSchedule.RowHeadersWidth = 51;
			dataGridViewSchedule.RowTemplate.Height = 29;
			dataGridViewSchedule.Size = new Size(856, 336);
			dataGridViewSchedule.TabIndex = 0;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(640, 416);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(232, 53);
			buttonExit.TabIndex = 1;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(16, 8);
			label1.Name = "label1";
			label1.Size = new Size(177, 41);
			label1.TabIndex = 2;
			label1.Text = "Расписание";
			// 
			// dateTimePickerScheduleDate
			// 
			dateTimePickerScheduleDate.Location = new Point(216, 16);
			dateTimePickerScheduleDate.Name = "dateTimePickerScheduleDate";
			dateTimePickerScheduleDate.Size = new Size(250, 27);
			dateTimePickerScheduleDate.TabIndex = 3;
			dateTimePickerScheduleDate.Value = new DateTime(2023, 12, 26, 23, 14, 9, 0);
			// 
			// buttonRefreshTable
			// 
			buttonRefreshTable.Location = new Point(480, 8);
			buttonRefreshTable.Name = "buttonRefreshTable";
			buttonRefreshTable.Size = new Size(144, 37);
			buttonRefreshTable.TabIndex = 4;
			buttonRefreshTable.Text = "Обновить";
			buttonRefreshTable.UseVisualStyleBackColor = true;
			buttonRefreshTable.Click += ButtonRefreshTable_Click;
			// 
			// buttonTrainerSchedule
			// 
			buttonTrainerSchedule.Location = new Point(696, 8);
			buttonTrainerSchedule.Name = "buttonTrainerSchedule";
			buttonTrainerSchedule.Size = new Size(168, 37);
			buttonTrainerSchedule.TabIndex = 5;
			buttonTrainerSchedule.Text = "Личное расписание";
			buttonTrainerSchedule.UseVisualStyleBackColor = true;
			buttonTrainerSchedule.Click += ButtonTrainerSchedule_Click;
			// 
			// ShowSchedule
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(886, 496);
			Controls.Add(buttonTrainerSchedule);
			Controls.Add(buttonRefreshTable);
			Controls.Add(dateTimePickerScheduleDate);
			Controls.Add(label1);
			Controls.Add(buttonExit);
			Controls.Add(dataGridViewSchedule);
			Name = "ShowSchedule";
			Text = "ShowSchedule";
			((System.ComponentModel.ISupportInitialize)dataGridViewSchedule).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridViewSchedule;
		private Button buttonExit;
		private Label label1;
		private DateTimePicker dateTimePickerScheduleDate;
		private Button buttonRefreshTable;
		private Button buttonTrainerSchedule;
	}
}