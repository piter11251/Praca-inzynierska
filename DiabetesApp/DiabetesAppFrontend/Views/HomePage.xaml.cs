
using Android.Graphics;
using AndroidX.Navigation;
using Demo.ApiClient;
using Microcharts;
using Microsoft.Maui.Graphics.Text;
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

        
        var mySugarData = new List<SugarData>
        {
            new SugarData { DayNumber = "1", SugarValue = 120},
            new SugarData { DayNumber = "2", SugarValue = 95},
            new SugarData { DayNumber = "3", SugarValue = 140},
            new SugarData { DayNumber = "4", SugarValue = 150},
            new SugarData { DayNumber = "5", SugarValue = 110},
        };

        chart.TooltipBehavior = new ChartTooltipBehavior
        {
            Duration = 5000,
            Background = Colors.Black,
            TextColor = Colors.White,

        };
        ChartTrackballBehavior trackball = new ChartTrackballBehavior
        {
            ShowLine = false,
            DisplayMode = LabelDisplayMode.FloatAllPoints,
        };
        chart.TrackballBehavior = trackball;

        var lineSeries = new LineSeries
        {
            ItemsSource = mySugarData,
            EnableTooltip = true,
            XBindingPath = "DayNumber",
            YBindingPath = "SugarValue",
            MarkerSettings = new ChartMarkerSettings
            {
                Type = ShapeType.Circle,

                Fill = new SolidColorBrush(Colors.Red),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeWidth = 3,
                Width = 10,
                Height = 10,
            },


            ShowDataLabels = true,
            DataLabelSettings = new CartesianDataLabelSettings
            {
                LabelStyle = new ChartDataLabelStyle
                {
                    TextColor = Colors.Black,
                    FontSize = 12
                }
            }
        };
        chart.Series.Add(lineSeries);


        

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var token = await SecureStorage.GetAsync("auth_token");
        if (string.IsNullOrEmpty(token))
        {
            await DisplayAlert("Blad", "Nie jestes zalogowany. Zaloguj sie, aby uzyskac dostep", "OK");
            Application.Current.MainPage = new NavigationPage(new LoginPage(_apiService));
            return;
        }
        try
        {
            var parts = token.Split('.');
            if (parts.Length < 2)
            {
                throw new Exception("Nieprawidlowy token");
            }

            var payload64 = parts[1];
            switch(payload64.Length % 4)
            {
                case 2: payload64 += "=="; break;
                case 3: payload64 += "="; break;
            }
            var jsonPayload = Encoding.UTF8.GetString(Convert.FromBase64String(payload64));
            Console.WriteLine("Payload JSON: " + jsonPayload);
            var claims = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonPayload);
            if(claims != null)
            {
                string firstNameKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
                string firstName = claims.TryGetValue(firstNameKey, out var value) ? value?.ToString() : "Unknown";
                WelcomeLabel.Text = $"Zalogowano {firstName}";
            }
            else
            {
                throw new Exception("Nie udalo sie rozkodowac tokena");
            }
           
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Blad autoryzacji: {ex.Message}");
            await DisplayAlert("Blad", "Sesja wygasla. Zaloguj sie ponownie.", "OK");
            Application.Current.MainPage = new NavigationPage(new LoginPage(_apiService));
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