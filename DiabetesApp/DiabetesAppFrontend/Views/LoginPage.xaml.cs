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

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ErrorMessageLabel.Text = "Proszê wprowadziæ email i haslo";
            ErrorMessageLabel.IsVisible = true;
            return;
        }

        try
        {
            var token = await _apiService.LoginAsync(email, password);
            if (!string.IsNullOrEmpty(token))
            {
                SecureStorage.SetAsync("auth_token", token);
                Application.Current.MainPage = new SugarEntryFlyoutPage(_apiService);
            }
        }
        catch (Exception ex)
        {
            ErrorMessageLabel.Text = $"Wystapil blad: {ex.Message}";
            ErrorMessageLabel.IsVisible = true;
        }
    }

    private async void OnRegisterLinkTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage(_apiService));
    }
}