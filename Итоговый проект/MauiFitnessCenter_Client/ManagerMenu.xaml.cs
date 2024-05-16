namespace MauiFitnessCenter_Client;

public partial class ManagerMenu : ContentPage
{
	User user;
	public ManagerMenu(User user)
	{
		InitializeComponent();
		this.user = user;
	}

	private async void buttonEmpAcc_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EmployeeAcc(user));
	}

	private void ButtonExit_Click(object sender, EventArgs e)
	{
		Application.Current.Quit();
	}

	private async void ButtonShowTickets_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowAbonements());
	}

	private async void ButtonShowSchedule_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowSchedule(user));
	}

	private async void ButtonShowTrainers_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowTrainers());
	}

	private async void ButtonShowClients_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowClients());
	}

	private async void ButtonManageTickets_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManageAbonements(user));
	}

	private async void ButtonManageSchedule_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManageSchedule(user));
	}

	private async void ButtonManageClients_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManageUsers(user));
	}
}