using Demo.ApiClient;
using Demo.ApiClient.Models;
using DiabetesAppFrontend.Views;

namespace DiabetesAppFrontend
{
    public partial class App : Application
    {
        private readonly DemoApiClientService _apiService;
        public App(DemoApiClientService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            MainPage = DetermineMainPage();
        }

        private Page DetermineMainPage()
        {
            var token = SecureStorage.GetAsync("auth_token").Result;
            if(!string.IsNullOrEmpty(token))
            {
                return new SugarEntryFlyoutPage(_apiService);
            }
            return new NavigationPage(new LandingPage(_apiService));
        }
    }
}
