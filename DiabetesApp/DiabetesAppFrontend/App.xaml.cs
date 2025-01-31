using Demo.ApiClient;
using Demo.ApiClient.Models;
using DiabetesAppFrontend.Views;

namespace DiabetesAppFrontend
{
    public partial class App : Application
    {
        private readonly DemoApiClientService _apiService;
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY5NDg4OEAzMjM4MmUzMDJlMzBQeHNQSk12V0JIaDZEWitHZ2gzQ0l3cEVKei9yUzV5Z09OTmpIWWZta3ljPQ==");
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();
            var token = SecureStorage.GetAsync("auth_token").Result;
            if(!string.IsNullOrEmpty(token))
            {
                Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                Shell.Current.GoToAsync("//LandingPage");
            }
        }
    }
}
