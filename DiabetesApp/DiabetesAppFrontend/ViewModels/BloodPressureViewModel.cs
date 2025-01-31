using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class BloodPressureViewModel: ObservableObject
    {
        private readonly DemoApiClientService _apiService;

        [ObservableProperty] private string stolicPressure;
        [ObservableProperty] private string diastolicPressure;
        [ObservableProperty] private string pulse;
        [ObservableProperty] private bool hasIrregularPulse;
        [ObservableProperty] private DateTime date = DateTime.Now;
        [ObservableProperty] private TimeSpan time = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string errorMessage;

        public BloodPressureViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        private async Task SendEntryAsync()
        {
            ErrorMessage = string.Empty;
            if(!int.TryParse(StolicPressure, out int stolic) || 
                !int.TryParse(DiastolicPressure, out int diastolic) || 
                !int.TryParse(Pulse, out int pulseVal) ||
                stolic <= 0 || diastolic <= 0 || pulseVal <= 0 || stolic < diastolic)
            {
                ErrorMessage = "Wprowadź poprawne wartości ciśnienia";
                return;
            }

            DateTime fullDate = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
            var dto = new BloodPressureDto
            {
                MeasurementDate = fullDate,
                StolicPressure = stolic,
                DiastolicPressure = diastolic,
                Pulse = pulseVal,
                IsIrregularPulse = hasIrregularPulse
            };

            try
            {
                await _apiService.AddBloodPressureAsync(dto);
                await Shell.Current.DisplayAlert("Sukces", "Pomiar ciśnienia", "OK");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Błąd: {ex.Message}";
            }
        }
    }
}
