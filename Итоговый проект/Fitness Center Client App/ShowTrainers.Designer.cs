namespace Fitness_Center_Client_App
{
	partial class ShowTrainers
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
			dataGridViewTrainers = new DataGridView();
			buttonExit = new Button();
			((System.ComponentModel.ISupportInitialize)dataGridViewTrainers).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(200, 0);
			label1.Name = "label1";
			label1.Size = new Size(368, 46);
			label1.TabIndex = 0;
			label1.Text = "Список всех тренеров";
			// 
			// dataGridViewTrainers
			// 
			dataGridViewTrainers.AllowUserToAddRows = false;
			dataGridViewTrainers.AllowUserToDeleteRows = false;
			dataGridViewTrainers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewTrainers.Location = new Point(16, 48);
			dataGridViewTrainers.Name = "dataGridViewTrainers";
			dataGridViewTrainers.ReadOnly = true;
			dataGridViewTrainers.RowHeadersWidth = 51;
			dataGridViewTrainers.RowTemplate.Height = 29;
			dataGridViewTrainers.Size = new Size(776, 384);
			dataGridViewTrainers.TabIndex = 1;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(592, 448);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(200, 48);
			buttonExit.TabIndex = 2;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// ShowTrainers
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(810, 515);
			Controls.Add(buttonExit);
			Controls.Add(dataGridViewTrainers);
			Controls.Add(label1);
			Name = "ShowTrainers";
			Text = "ShowTrainers";
			((System.ComponentModel.ISupportInitialize)dataGridViewTrainers).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private DataGridView dataGridViewTrainers;
		private Button buttonExit;
	}
}