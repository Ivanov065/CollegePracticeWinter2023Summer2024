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
	public partial class ClientMenu : Form
	{
		Form parent;
		User user;
		ClientAcc clientAcc = null;
		ShowAbonements showAbonements = null;
		ShowSchedule showSchedule = null;
		public ClientMenu(Form parentForm, User user)
		{
			parent = parentForm;
			this.user = user;
			InitializeComponent();
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonShowClientAcc_Click(object sender, EventArgs e)
		{
			if (clientAcc == null) clientAcc = new ClientAcc(this, user);
			clientAcc.FormClosed += ClientAcc_FormClosed;
			clientAcc.Show();
			Hide();
		}

		private void ClientAcc_FormClosed(object sender, EventArgs e)
		{
			clientAcc = null;
			this.Show();
		}

		private void ButtonShowSeasonTickets_Click(object sender, EventArgs e)
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
	}
}
