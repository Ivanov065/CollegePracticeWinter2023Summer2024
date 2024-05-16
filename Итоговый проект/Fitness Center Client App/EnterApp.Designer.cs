namespace Fitness_Center_Client_App
{
	partial class EnterApp
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			label2 = new Label();
			textBoxLogin = new TextBox();
			label3 = new Label();
			textBoxPassword = new TextBox();
			buttonLogin = new Button();
			buttonExit = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(56, 8);
			label1.Name = "label1";
			label1.Size = new Size(315, 54);
			label1.TabIndex = 0;
			label1.Text = "Идентификация";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(32, 80);
			label2.Name = "label2";
			label2.Size = new Size(69, 28);
			label2.TabIndex = 1;
			label2.Text = "Логин";
			// 
			// textBoxLogin
			// 
			textBoxLogin.Location = new Point(32, 120);
			textBoxLogin.Name = "textBoxLogin";
			textBoxLogin.Size = new Size(344, 27);
			textBoxLogin.TabIndex = 2;
			textBoxLogin.TextChanged += LoginTextBoxes_TextChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(32, 168);
			label3.Name = "label3";
			label3.Size = new Size(81, 28);
			label3.TabIndex = 3;
			label3.Text = "Пароль";
			// 
			// textBoxPassword
			// 
			textBoxPassword.Location = new Point(32, 208);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.PasswordChar = '*';
			textBoxPassword.Size = new Size(344, 27);
			textBoxPassword.TabIndex = 4;
			textBoxPassword.TextChanged += LoginTextBoxes_TextChanged;
			// 
			// buttonLogin
			// 
			buttonLogin.Location = new Point(240, 264);
			buttonLogin.Name = "buttonLogin";
			buttonLogin.Size = new Size(136, 40);
			buttonLogin.TabIndex = 7;
			buttonLogin.Text = "Войти";
			buttonLogin.UseVisualStyleBackColor = true;
			buttonLogin.Click += ButtonLogin_Click;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(32, 264);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(136, 40);
			buttonExit.TabIndex = 8;
			buttonExit.Text = "Выйти";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += ButtonExit_Click;
			// 
			// EnterApp
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(415, 346);
			Controls.Add(buttonExit);
			Controls.Add(buttonLogin);
			Controls.Add(textBoxPassword);
			Controls.Add(label3);
			Controls.Add(textBoxLogin);
			Controls.Add(label2);
			Controls.Add(label1);
			Name = "EnterApp";
			Text = "Фитнесс клуб - Вход";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label label2;
		private TextBox textBoxLogin;
		private Label label3;
		private TextBox textBoxPassword;
		private Button buttonLogin;
		private Button buttonExit;
	}
}