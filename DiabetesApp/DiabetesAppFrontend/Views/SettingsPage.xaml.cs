using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsPageViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if(BindingContext is SettingsPageViewModel vm)
        {
            await vm.LoadPreferencesAsync();
        }
    }
} 