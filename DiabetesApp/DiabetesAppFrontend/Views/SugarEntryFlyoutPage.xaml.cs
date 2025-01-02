using Demo.ApiClient;
using System.Runtime.InteropServices.Marshalling;

namespace DiabetesAppFrontend.Views;

public partial class SugarEntryFlyoutPage : FlyoutPage
{
	private readonly DemoApiClientService _apiService;
	public SugarEntryFlyoutPage(DemoApiClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
		Detail = new NavigationPage(new HomePage(_apiService));
		IsPresented = false;
	}

	public SugarEntryFlyoutPage()
	{
		InitializeComponent();
	}

	private async void NavigateToHome(object sender, EventArgs e)
	{
		Detail = new NavigationPage(new HomePage(_apiService));
		IsPresented = false;
	}

	private async void Logout(object sender, EventArgs e)
	{
		SecureStorage.Remove("auth_token");
		Application.Current.MainPage = new NavigationPage(new LandingPage(_apiService));
	}

	private async void NavigateToSugar(object sender, EventArgs e)
	{
		if(Detail is NavigationPage nav)
		{
			await nav.PushAsync(new SugarEntryView(_apiService));
		}
		IsPresented = false;
	}

	private async void NavigateToBloodPressure(object sender, EventArgs e)
	{
        if(Detail is NavigationPage nav)
		{
			await nav.PushAsync(new BloodPressurePage(_apiService));
		}
		IsPresented = false;
	}

    private async void NavigateToPhysicalActivities(object sender, EventArgs e)
	{
        Console.WriteLine("test1");
	}
}