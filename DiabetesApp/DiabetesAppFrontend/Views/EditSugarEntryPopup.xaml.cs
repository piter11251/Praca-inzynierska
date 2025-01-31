using Android.Views;
using Demo.ApiClient;
using CommunityToolkit.Maui.Views;
using Demo.ApiClient.Models.ApiModels;

namespace DiabetesAppFrontend.Views;

public partial class EditSugarEntryPopup : Popup
{
	private SugarEntry _entry;
	private readonly DemoApiClientService _apiService;
	public EditSugarEntryPopup(SugarEntry entry, DemoApiClientService apiService)
	{
		InitializeComponent();
		_entry = entry;
		_apiService = apiService;

		SugarValueEntry.Text = _entry.SugarValue.ToString();
		MealDatePicker.Date = _entry.MealTime.Date;
		MealTimePicker.Time = _entry.MealTime.TimeOfDay;
		MealMarkerPicker.SelectedItem = _entry.MealMarker;
	}

	public void OnCancelClicked(object sender, EventArgs e)
	{
		Close();
	}

	public async void OnSaveClicked(object sender, EventArgs e)
	{
		if(int.TryParse(SugarValueEntry.Text, out int newValue))
		{
			_entry.SugarValue = newValue;
		}

		var success = await _apiService.ModifyEntryAsync(_entry);
		var date = MealDatePicker.Date;
		var time = MealTimePicker.Time;
		_entry.MealTime = date + time;

		if(MealMarkerPicker.SelectedItem is string selectedMarker)
		{
			_entry.MealMarker = selectedMarker;
		}

		if(success)
		{
			Close();
		}
		else
		{
			await Application.Current.MainPage.DisplayAlert("B³¹d", "Nie uda³o siê zaktualizowaæ wpisu", "OK");
		}
	}
}