using CommunityToolkit.Maui.Views;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;

namespace DiabetesAppFrontend.Views;

public partial class EditBloodPressurePopup : Popup
{
	private BloodPressureEntry _entry;
	private DemoApiClientService _apiService;
	public EditBloodPressurePopup(BloodPressureEntry entry, DemoApiClientService apiService)
	{
		InitializeComponent();
		_entry = entry;
		_apiService = apiService;

		StolicEntry.Text = _entry.StolicValue.ToString();
		DiastolicEntry.Text = _entry.DiastolicValue.ToString();
		MeasureDatePicker.Date = _entry.MeasureTime.Date;
		MeasureTimePicker.Time = _entry.MeasureTime.TimeOfDay;
	}

	private void Cancel_Clicked(object sender, EventArgs e)
	{
		Close(false);
	}

	private async void Save_Clicked(object sender, EventArgs e)
	{
		if(int.TryParse(StolicEntry.Text, out int sVal))
		{
			_entry.StolicValue = sVal;
		}
		if(int.TryParse(DiastolicEntry.Text, out int dVal))
		{
			_entry.DiastolicValue = dVal;
		}

		_entry.MeasureTime = MeasureDatePicker.Date + MeasureTimePicker.Time;
		bool success = await _apiService.ModifyBloodPressureAsync(_entry);
		if(success)
		{
			Close(true);
		}
		else
		{
			await Application.Current.MainPage.DisplayAlert("B³¹d", "Nie uda³o sie zapisaæ zmian", "OK");
		}

	}
}