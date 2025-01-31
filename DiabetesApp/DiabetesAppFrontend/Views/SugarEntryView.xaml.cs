using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.ViewModels;


namespace DiabetesAppFrontend.Views;

public partial class SugarEntryView : ContentPage
{
	public SugarEntryView(SugarEntryViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}