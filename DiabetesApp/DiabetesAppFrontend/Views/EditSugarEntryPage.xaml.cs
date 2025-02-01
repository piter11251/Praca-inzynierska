using Android.Views;
using Demo.ApiClient;
using CommunityToolkit.Maui.Views;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class EditSugarEntryPage : ContentPage
{
    public EditSugarEntryPage(EditSugarEntryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}