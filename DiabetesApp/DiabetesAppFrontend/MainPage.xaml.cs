using Demo.ApiClient;
using DiabetesAppFrontend.Services;
using DiabetesAppFrontend.Views;

namespace DiabetesAppFrontend
{
    public partial class MainPage : ContentPage
    {
        

         private readonly DemoApiClientService _apiService;
         public MainPage(DemoApiClientService apiService)
         {
             InitializeComponent();
             _apiService = apiService;
         }

        /*protected override async void OnAppearing()
        {
            base.OnAppearing();
            var token = await _apiService.GetAuthTokenAsync();
            if(!string.IsNullOrEmpty(token))
            {
                await Navigation.PushAsync(new HomePage(_apiService));
            }
        }*/

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new LoginPage(_apiService), true);
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_apiService), true);
        }
        
        
    }

}
