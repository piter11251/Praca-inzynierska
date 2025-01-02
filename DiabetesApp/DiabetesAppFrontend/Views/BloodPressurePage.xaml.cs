using Demo.ApiClient;

namespace DiabetesAppFrontend.Views;

public partial class BloodPressurePage : ContentPage
{
	private DemoApiClientService _apiService;
	private bool _hasIrregularPulse;
	public BloodPressurePage(DemoApiClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService; 
	}

	public async void OnIrregularPulseChecked(object sender, CheckedChangedEventArgs e)
	{
		_hasIrregularPulse = e.Value;
	}

}