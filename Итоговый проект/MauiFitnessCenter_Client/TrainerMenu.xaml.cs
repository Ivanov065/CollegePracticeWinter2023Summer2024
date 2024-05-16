namespace MauiFitnessCenter_Client;

public partial class TrainerMenu : ContentPage
{
	User user;
	public TrainerMenu(User user)
	{
		InitializeComponent();
		this.user = user;
	}

	private void ButtonExit_Click(object sender, EventArgs e)
	{
		Application.Current.Quit();
	}

	private async void ButtonShowAcc_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EmployeeAcc(user));
	}

	private async void ButtonSchedule_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShowSchedule(user));
	}
}