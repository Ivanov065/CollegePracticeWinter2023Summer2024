namespace Fitness_Center_Client_App
{
	partial class ShowAbonements
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
			dataGridViewTickets = new DataGridView();
			buttonExit = new Button();
			label1 = new Label();
			((System.ComponentModel.ISupportInitialize)dataGridViewTickets).BeginInit();
			SuspendLayout();
			// 
			// dataGridViewTickets
			// 
			dataGridViewTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewTickets.Location = new Point(8, 56);
			dataGridViewTickets.Name = "dataGridViewTickets";
			dataGridViewTickets.ReadOnly = true;
			dataGridViewTickets.RowHeadersWidth = 51;
			dataGridViewTickets.RowTemplate.Height = 29;
			dataGridViewTickets.Size = new Size(800, 384);
			dataGridViewTickets.TabIndex = 0;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(616, 456);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(192, 48);
			buttonExit.TabIndex = 1;
			buttonExit.Text = "Вернуться назад";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(200, 0);
			label1.Name = "label1";
			label1.Size = new Size(406, 41);
			label1.TabIndex = 2;
			label1.Text = "Все Доступные Абонементы";
			// 
			// ShowAbonements
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(822, 517);
			Controls.Add(label1);
			Controls.Add(buttonExit);
			Controls.Add(dataGridViewTickets);
			Name = "ShowAbonements";
			Text = "ShowAbonements";
			((System.ComponentModel.ISupportInitialize)dataGridViewTickets).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridViewTickets;
		private Button buttonExit;
		private Label label1;
	}
}