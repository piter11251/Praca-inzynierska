using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.ApiClient;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        private readonly DemoApiClientService _apiService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public LoginViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Podaj email i haslo";
                return;
            }

            try
            {
                var token = await _apiService.LoginAsync(Email.Trim(), Password.Trim());
                if (!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.SetAsync("auth_token", token);
                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    ErrorMessage = "Niepoprawny login/haslo";
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = $"Błąd: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync("RegisterPage");
        }

    }
}
