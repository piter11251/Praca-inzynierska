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
        private int _userAge;
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
            _ = LoadUserProfileAsync();
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

            int minStolic, maxStolic, minDiastolic = 70, maxDiastolic = 79;
            if(_userAge <= 65)
            {
                minStolic = 120;
                maxStolic = 129;
            }
            else if(_userAge > 65 && _userAge <= 80)
            {
                minStolic = 130;
                maxStolic = 139;
            }
            else
            {
                minStolic = 130;
                maxStolic = 149;
            }
            bool outOfRangeStolic = (stolic < minStolic || stolic > maxStolic);
            bool outOfRangeDiastolic = (diastolic < minDiastolic || diastolic >  maxDiastolic);


            if(outOfRangeStolic || outOfRangeDiastolic)
            {
                await Shell.Current.DisplayAlert("Uwaga",
                    $"Twoje cisnienie ({stolic}/{diastolic} odbiega od " +
                    $"zalecanego zakresu {minStolic}-{maxStolic}/" +
                    $"{minDiastolic}/{maxDiastolic}). Zwróć uwagę na samopoczucie.",
                    "OK");
            }

            if(pulseVal < 60 && pulseVal > 100)
            {
                await Shell.Current.DisplayAlert("Nieprawidłowy puls",
                    $"Puls {pulseVal} jest nieprawidłowy. Obserwuj samopoczucie.",
                    "OK");
            }

            if(HasIrregularPulse && (outOfRangeStolic || outOfRangeDiastolic))
            {
                await Shell.Current.DisplayAlert("Pilne",
                    "Masz nieregularny puls przy wysokim ciśnieniu! " +
                    "Skonsultuj się z lekarzem.",
                    "OK");
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
                await Shell.Current.DisplayAlert("Sukces", "Pomiar ciśnienia zapisany", "OK");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Błąd: {ex.Message}";
            }
        }

        private async Task LoadUserProfileAsync()
        {
            var profile = await _apiService.GetUserProfileAsync();
            if(profile != null)
            {
                _userAge = CalculateAge(profile.DateOfBirth);
                Console.WriteLine("Wiek usera: " + _userAge);
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
}
