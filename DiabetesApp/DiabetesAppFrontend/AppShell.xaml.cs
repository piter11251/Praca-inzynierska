using Demo.ApiClient;
using DiabetesAppFrontend.Views;

namespace DiabetesAppFrontend
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute(nameof(EditSugarEntryPage), typeof(EditSugarEntryPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

       private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("auth_token");
            await Shell.Current.GoToAsync("//LandingPage");
        }

    }
}
