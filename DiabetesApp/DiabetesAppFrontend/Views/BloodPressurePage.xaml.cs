using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;

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

	public async void OnSendEntryClicked(object sender, EventArgs e)
	{
		var isDiastolicTrue = int.TryParse(DiastolicPressure.Text, out int diastolicPressure);
		var isStolicTrue = int.TryParse(StolicPressure.Text, out int stolicPressure);
		if (string.IsNullOrEmpty(DiastolicPressure.Text) || isDiastolicTrue == false || isStolicTrue == false || stolicPressure < diastolicPressure || stolicPressure <= 0 || diastolicPressure <= 0 || !int.TryParse(Pulse.Text, out int pulse) || pulse <= 0)
		{
            await DisplayAlert("Blad", "Wprowadz poprawny pomiar cisnienia.", "ok");
            return;
        }
		var hasIrregularPulse = IrregularPulseCheckbox.IsChecked;
        DateTime date = EntryDate.Date;
        TimeSpan time = SelectedTime.Time;
        DateTime fulldate = new DateTime
        (
            date.Year,
            date.Month,
            date.Day,
            time.Hours,
            time.Minutes,
            0
        );

		var bloodPressureData = new BloodPressureDto
		{
			MeasurementDate = fulldate,
			StolicPressure = stolicPressure,
			DiastolicPressure = diastolicPressure,
			Pulse = pulse,
			IsIrregularPulse = hasIrregularPulse
		};

		try
		{
			await _apiService.AddBloodPressureAsync(bloodPressureData);
            await DisplayAlert("Sukces", "Pomiar zostal wyslany", "ok");
        }
		catch(Exception ex)
		{
			await DisplayAlert("Blad", $"Wystapil blad podczas przesylania danych: {ex.Message}", "Ok");
		}
    }

}