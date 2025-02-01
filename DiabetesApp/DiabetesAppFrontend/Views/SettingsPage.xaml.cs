using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}