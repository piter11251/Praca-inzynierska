
using Demo.ApiClient;
using Demo.ApiClient.IoC;
using DiabetesAppFrontend.Services;
using DiabetesAppFrontend.Views;
using Microsoft.Extensions.Logging;

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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddDemoApiClientService(x => x.BaseApiAddress = "http://10.0.2.2:5265");
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton(async provider =>
            {
                var client = provider.GetRequiredService<DemoApiClientService>();
                await client.InitializeAuthTokenAsync();
                return client;
            });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
