using Demo.ApiClient;
//using DiabetesAppFrontend.Models;
using System.Text.RegularExpressions;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}