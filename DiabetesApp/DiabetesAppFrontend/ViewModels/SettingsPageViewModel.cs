using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class SettingsPageViewModel: ObservableObject
    {
        public SettingsPageViewModel()
        {
            SugarLow = AppPreferences.SugarLow;
            SugarHigh = AppPreferences.SugarHigh;
            BloodPressureLow = AppPreferences.BloodPressureLow;
            BloodPressureHigh = AppPreferences.BloodPressureHigh;

        }

        [ObservableProperty]
        private int sugarLow;

        [ObservableProperty] 
        private int sugarHigh;

        [ObservableProperty]
        private int bloodPressureLow;

        [ObservableProperty]
        private int bloodPressureHigh;

        [RelayCommand]
        private async Task SaveAsync()
        {
            AppPreferences.SugarLow = SugarLow;
            AppPreferences.SugarHigh = SugarHigh;
            AppPreferences.BloodPressureLow = BloodPressureLow;
            AppPreferences.BloodPressureHigh = BloodPressureHigh;

            await App.Current.MainPage.DisplayAlert("Sukces", "Ustawienia zapisane", "Ok");
        }
    }
}
