using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;


namespace DiabetesAppFrontend.Views;

public partial class SugarEntryView : ContentPage
{
	private readonly DemoApiClientService _apiService;
	public SugarEntryView(DemoApiClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
		NavigationPage.SetHasBackButton(this, true);
		NavigationPage.SetHasNavigationBar(this, true);
		Title = "";
	}

    public SugarEntryView()
    {
		InitializeComponent();
    }

    private void OnToggleFlyoutClicked(object sender, EventArgs e)
	{
		if(Application.Current.MainPage is FlyoutPage flyout)
		{
			flyout.IsPresented = !flyout.IsPresented;
		}
		else
            Console.WriteLine("Current Mainpage is not a flyoutpage");
	}
    private async void OnSendEntryClicked(object sender, EventArgs e)
	{
		int sugarLevel = 0;
		if(string.IsNullOrWhiteSpace(SugarEntry.Text) || 
			!int.TryParse(SugarEntry.Text, out sugarLevel) || 
			sugarLevel <= 0)
		{
			await DisplayAlert("Blad", "Wprowadz poprawny dodatni pomiar cukru.", "ok");
			return;
		}

		if(MealType.SelectedItem == null || MealMarker.SelectedItem == null)
		{
            await DisplayAlert("Blad", "Wprowadz typ posilku oraz pore posilku.", "ok");
			return;

        }
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
		var sugarEntryDto = new SugarEntryDto
		{
			SugarValue = sugarLevel,
			MealTime = fulldate,
			MealType = MealType.SelectedIndex,
			MealMarker = MealMarker.SelectedIndex
		};

		try
		{
			await _apiService.AddSugarEntryAsync(sugarEntryDto);
			await DisplayAlert("Sukces", "Pomiar zostal wyslany", "ok");
		}
		catch(Exception ex)
		{
			await DisplayAlert("Blad", $"Wystapil blad poczas wysylania danych: {ex.Message}", "ok");
		}
	}

    protected override bool OnBackButtonPressed()
    {
		Navigation.PopAsync();
		return true;
    }

}