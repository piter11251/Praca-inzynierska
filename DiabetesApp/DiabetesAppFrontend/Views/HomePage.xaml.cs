
using AndroidX.Navigation;
using Demo.ApiClient;
using Microcharts;
using SkiaSharp;
using Syncfusion.Maui.Charts;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DiabetesAppFrontend.Views;

public partial class HomePage : ContentPage
{
    private readonly DemoApiClientService _apiService;
    public List<SugarData> MySugarData { get; set; }

    public HomePage(DemoApiClientService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, true);
        Title = "";

        var chart = new SfCartesianChart
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand
        };

        var categoryAxis = new CategoryAxis
        {
            Title = new ChartAxisTitle
            {
                Text = "Dzien pomiaru"
            }
        };

        chart.XAxes.Add(categoryAxis);

        var numericalAxis = new NumericalAxis
        {
            Title = new ChartAxisTitle
            {
                Text = "Cukier"
            },
            Minimum = 40,
            Maximum = 150,
            Interval = 50
        };

        var mySugarData = new List<SugarData>
        {
            new SugarData { DayNumber = "1", SugarValue = 120},
            new SugarData { DayNumber = "2", SugarValue = 95},
            new SugarData { DayNumber = "3", SugarValue = 140},
            new SugarData { DayNumber = "4", SugarValue = 150},
            new SugarData { DayNumber = "5", SugarValue = 110},
        };

        var splineSeries = new SplineSeries
        {
            ItemsSource = mySugarData,
            XBindingPath = "DayNumber",
            YBindingPath = "SugarValue",
            Type = SplineType.Cardinal,
            Label = "Wartosci",
            ShowDataLabels = true
        };

        chart.Series.Add(splineSeries);
        ChartContainer.Content = chart;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var token = await _apiService.GetAuthTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            var parts = token.Split('.');
            if (parts.Length < 2) return;

            var payload64 = parts[1];
            switch(payload64.Length % 4)
            {
                case 2: payload64 += "=="; break;
                case 3: payload64 += "="; break;
            }
            var jsonPayload = Encoding.UTF8.GetString(Convert.FromBase64String(payload64));
            Console.WriteLine("Payload JSON: " + jsonPayload);
            var claims = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonPayload);
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim: {claim.Key} - {claim.Value}");
            }
            string firstNameKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
            string firstName = claims.TryGetValue(firstNameKey, out var value) ? value?.ToString() : "Unknown";
            WelcomeLabel.Text = $"Zalogowano {firstName}";
        }
        else
        {
            Application.Current.MainPage = new NavigationPage(new LandingPage(_apiService));
        }
    }

    private SKColor GetColor(int value)
    {
        if (value < 70) return SKColor.Parse("#FF0000");
        if (value > 110) return SKColor.Parse("#FFA500");
        return SKColor.Parse("#008000");
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        SecureStorage.Remove("auth_token");
        var token = await SecureStorage.GetAsync("auth_token");
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("usuniety token");
        }
        else
        {
            Console.WriteLine("token nie usuniety");
        }
        Application.Current.MainPage = new NavigationPage(new LandingPage(_apiService));
    }

    private async void OnSugarButtonClicked(object sender, EventArgs e)
    {
        if(Application.Current.MainPage is FlyoutPage flyout)
        {
            flyout.IsPresented = false;
        }
        await Navigation.PushAsync(new SugarEntryView(_apiService), false);
    }

    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            bool confirm = await DisplayAlert("Zamkniecie", "Czy chcesz zamkn¹æ aplikacje?", "Tak", "Nie");
            if (confirm)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        });
        return true;
    }
}

public class SugarData
{
    public string DayNumber { get; set; }
    public double SugarValue { get; set; }
}