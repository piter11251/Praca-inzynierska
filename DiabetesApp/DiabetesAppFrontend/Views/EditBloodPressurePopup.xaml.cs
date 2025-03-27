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
        var vm = new EditBloodPressureViewModel(entry, apiService);
        vm.RequestClose += (s, e) => Close();
        BindingContext = new EditBloodPressureViewModel(entry, apiService);
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}