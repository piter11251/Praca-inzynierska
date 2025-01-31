
using Demo.ApiClient;
using Demo.ApiClient.IoC;
using DiabetesAppFrontend.Views;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;
using DiabetesAppFrontend.ViewModels;
using DiabetesAppFrontend.Services;

namespace DiabetesAppFrontend
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            AppContext.SetSwitch("System.Reflection.NullabilityInfoContext.IsSupported", true);
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
                })
                .ConfigureSyncfusionCore();
            builder.Services.AddDemoApiClientService(x => x.BaseApiAddress = "http://10.0.2.2:5265");
            builder.Services.AddTransient<LandingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton(async provider =>
            {
                var client = provider.GetRequiredService<DemoApiClientService>();
                await client.InitializeAuthTokenAsync();
                return client;
            });
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<SugarEntryView>();
            builder.Services.AddTransient<SugarEntryViewModel>();

            builder.Services.AddTransient<BloodPressurePage>();
            builder.Services.AddTransient<BloodPressureViewModel>();

            builder.Services.AddTransient<LandingPage>();
            builder.Services.AddTransient<LandingViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}