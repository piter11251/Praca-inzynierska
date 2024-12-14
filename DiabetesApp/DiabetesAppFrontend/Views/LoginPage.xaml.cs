using Demo.ApiClient;

namespace DiabetesAppFrontend.Views;

public partial class LoginPage : ContentPage
{
	private readonly DemoApiClientService _apiService;
	public LoginPage(DemoApiClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
	}

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		var email = EmailEntry.Text?.Trim();
		var password = PasswordEntry.Text?.Trim();

		if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
		{
			ErrorMessageLabel.Text = "Prosz� wprowadzi� email i has�o";
			ErrorMessageLabel.IsVisible = true;
			return;
		}

		try
		{
			var token = await _apiService.LoginAsync(email, password);
			if (!string.IsNullOrEmpty(token))
			{
				await Navigation.PushAsync(new HomePage(_apiService));
			}
		}
		catch (Exception ex)
		{
			ErrorMessageLabel.Text = $"Wyst�pi� b��d: {ex.Message}";
			ErrorMessageLabel.IsVisible = true;
		}
	}
}