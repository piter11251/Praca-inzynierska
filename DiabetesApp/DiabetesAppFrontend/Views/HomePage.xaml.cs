using Demo.ApiClient;

namespace DiabetesAppFrontend.Views;

public partial class HomePage : ContentPage
{
	private readonly DemoApiClientService _apiService;
	public HomePage(DemoApiClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		var token = await _apiService.GetAuthTokenAsync();
		if(!string.IsNullOrEmpty(token))
		{
			var userEmail = "User";
			WelcomeLabel.Text = $"Zalogowano, {userEmail}";
		}
		else
		{
			await Navigation.PushAsync(new MainPage(_apiService));
		}
    }

	private async void OnLogoutButtonClicked(object sender, EventArgs e)
	{
		SecureStorage.Remove("auth_token");
		var token = await SecureStorage.GetAsync("auth_token");
		if(string.IsNullOrEmpty(token))
		{
            Console.WriteLine("usuniety token");
		}
		else
		{
            Console.WriteLine("token nie usuniety");
		}
		Application.Current.MainPage = new NavigationPage(new MainPage(_apiService));
	}
}