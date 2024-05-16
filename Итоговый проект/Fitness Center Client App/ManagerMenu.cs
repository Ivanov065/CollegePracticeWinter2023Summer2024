using Microsoft.VisualBasic.ApplicationServices;
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
	public partial class ManagerMenu : Form
	{
		Form parent;
		User user;
		EmployeeAcc employeeAcc = null;
		ShowAbonements showAbonements = null;
		ShowSchedule showSchedule = null;
		ShowTrainers showTrainers = null;
		ShowClients showClients = null;
		ManageAbonements manageAbonements = null;
		ManageSchedule manageSchedule = null;
		ManageUsers manageUsers = null;


		public ManagerMenu(Form parent, User user)
		{
			InitializeComponent();
			this.parent = parent;
			this.user = user;
		}

		private void buttonEmpAcc_Click(object sender, EventArgs e)
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

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonShowTickets_Click(object sender, EventArgs e)
		{
			if (showAbonements == null) showAbonements = new ShowAbonements(this);
			showAbonements.FormClosed += ShowAbonements_FormClosed;
			showAbonements.Show();
			Hide();
		}

		private void ShowAbonements_FormClosed(object sender, EventArgs e)
		{
			showAbonements = null;
			this.Show();
		}

		private void ButtonShowSchedule_Click(object sender, EventArgs e)
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

		private void ButtonShowTrainers_Click(object sender, EventArgs e)
		{
			if (showTrainers == null) showTrainers = new ShowTrainers(this);
			showTrainers.FormClosed += ShowTrainers_FormClosed;
			showTrainers.Show();
			Hide();
		}

		private void ShowTrainers_FormClosed(object sender, EventArgs e)
		{
			showTrainers = null;
			this.Show();
		}

		private void ButtonShowClients_Click(object sender, EventArgs e)
		{
			if (showClients == null) showClients = new ShowClients(this);
			showClients.FormClosed += ShowClients_FormClosed;
			showClients.Show();
			Hide();
		}

		private void ShowClients_FormClosed(object sender, EventArgs e)
		{
			showClients = null;
			this.Show();
		}

		private void ButtonManageTickets_Click(object sender, EventArgs e)
		{
			if (manageAbonements == null) manageAbonements = new ManageAbonements(this, user);
			manageAbonements.FormClosed += ManageAbonements_FormClosed;
			manageAbonements.Show();
			Hide();
		}
		private void ManageAbonements_FormClosed(object sender, EventArgs e)
		{
			manageAbonements = null;
			this.Show();
		}

		private void ButtonManageSchedule_Click(object sender, EventArgs e)
		{
			if (manageSchedule == null) manageSchedule = new ManageSchedule(this, user);
			manageSchedule.FormClosed += ManageSchedule_FormClosed;
			manageSchedule.Show();
			Hide();
		}

		private void ManageSchedule_FormClosed(object sender, EventArgs e)
		{
			manageSchedule = null;
			this.Show();
		}

		private void ButtonManageClients_Click(object sender, EventArgs e)
		{
			if (manageUsers == null) manageUsers = new ManageUsers(this, user);
			manageUsers.FormClosed += ManageUsers_FormClosed;
			manageUsers.Show();
			Hide();
		}

		private void ManageUsers_FormClosed(object sender, EventArgs e)
		{
			manageUsers = null;
			this.Show();
		}
	}
}
