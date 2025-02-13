using CommunityToolkit.Maui.Views;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.ViewModels;

namespace DiabetesAppFrontend.Views;

public partial class EditBloodPressurePopup : Popup
{
    public EditBloodPressurePopup(BloodPressureDto entry, DemoApiClientService apiService)
    {
        InitializeComponent();
        BindingContext = new EditBloodPressureViewModel(entry, apiService);
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}