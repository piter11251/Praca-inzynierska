using Android.Views;
using Demo.ApiClient;
using CommunityToolkit.Maui.Views;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class EditSugarEntryPopup : Popup
{
    public EditSugarEntryPopup(SugarEntry sugarEntry, DemoApiClientService apiService)
    {
        InitializeComponent();
        BindingContext = new EditSugarEntryViewModel(sugarEntry, apiService);
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}