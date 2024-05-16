using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness_Center_Client_App
{
	public partial class TrainerMenu : Form
	{
		User user;
		Form parentForm;
		EmployeeAcc employeeAcc = null;
		ShowSchedule showSchedule = null;
		public TrainerMenu(Form parentForm, User user)
		{
			InitializeComponent();
			this.user = user;
			this.parentForm = parentForm;
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonShowAcc_Click(object sender, EventArgs e)
		{
			if (employeeAcc == null) employeeAcc = new EmployeeAcc(this, user);
			employeeAcc.FormClosed += EmployeeAcc_FormClosed;
			employeeAcc.Show();
			Hide();
		}

		private void EmployeeAcc_FormClosed(object sender, EventArgs e)
		{
			employeeAcc = null;
			this.Show();
		}

		private void ButtonSchedule_Click(object sender, EventArgs e)
		{
			if (showSchedule == null) showSchedule = new ShowSchedule(this, user);
			showSchedule.FormClosed += ShowSchedule_FormClosed;
			showSchedule.Show();
			Hide();
		}

		private void ShowSchedule_FormClosed(object sender, EventArgs e)
		{
			showSchedule = null;
			this.Show();
		}
	}
}
