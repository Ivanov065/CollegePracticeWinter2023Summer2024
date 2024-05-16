namespace MauiFitnessCenter_Client;

public partial class ClientMenu : ContentPage
{
	User user;
	public ClientMenu(User user)
	{
		InitializeComponent();
		this.user = user;
	}

	private void ButtonExit_Click(object sender, EventArgs e)
	{
		Application.Current.Quit();
	}

	private async void ButtonShowClientAcc_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ClientAcc(user));
	}

	private async void ButtonShowSeasonTickets_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowAbonements());
	}

	private async void ButtonShowSchedule_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowSchedule(user));
	}
}