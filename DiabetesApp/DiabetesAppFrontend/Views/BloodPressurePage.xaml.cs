using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class BloodPressurePage : ContentPage
{
	public BloodPressurePage(BloodPressureViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}