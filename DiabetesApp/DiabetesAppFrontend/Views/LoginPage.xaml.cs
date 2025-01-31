using CommunityToolkit.Mvvm.DependencyInjection;
using Demo.ApiClient;
using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    public LoginPage()
    {
        InitializeComponent();
        var vm = Ioc.Default.GetRequiredService<LoginViewModel>();
        BindingContext = vm;
    }
}