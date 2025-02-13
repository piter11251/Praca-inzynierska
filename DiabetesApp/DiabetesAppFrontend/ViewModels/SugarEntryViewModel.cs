using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.Enums;
using DiabetesAppFrontend.Helpers;
using DiabetesAppFrontend.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class SugarEntryViewModel: ObservableObject
    {
        private readonly DemoApiClientService _apiService;

        [ObservableProperty] private string sugarValue;
        [ObservableProperty] private DateTime date = DateTime.Now;
        [ObservableProperty] private TimeSpan time = DateTime.Now.TimeOfDay;
        [ObservableProperty] private int selectedMealType;
        [ObservableProperty] private int selectedMealMarker;
        [ObservableProperty] private string errorMessage;
        public ObservableCollection<SugarEntry> SugarEntries { get; set; }
        public ObservableCollection<BloodPressureDto> BloodPressures { get; set; }
        private GetUserPreferencesDto _userPreferences;

        public SugarEntryViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;

        }

        public async Task LoadUserPreferencesAsync()
        {
            _userPreferences = await _apiService.GetUserPreferencesAsync();
        }

        [RelayCommand]
        private async Task SendEntryAsync()
        {
            
            ErrorMessage = string.Empty;
            if(string.IsNullOrEmpty(SugarValue) || !int.TryParse(SugarValue, out int sugar) || sugar <= 0)
            {
                ErrorMessage = "Wprowadź poprawny dodatni pomiar cukru";
                return;
            }

            if(_userPreferences == null)
            {
                await Application.Current.MainPage.DisplayAlert("Blad", "Nie udalo sie pobrac preferencji uzytkownika", "Ok");
                return;
            }

            string markerString = ((MealMarker) selectedMealMarker).ToString();

            var markerPrefs = _userPreferences.PrefelableSugarLevels.Find(p => p.MealMarker == markerString);
            if (markerPrefs == null)
            {
                await Application.Current.MainPage.DisplayAlert("Blad", $"Brak preferencji dla markeru: {selectedMealMarker}", "Ok");
                return;
            }

            if(sugar < markerPrefs.MinValue)
            {
                await Application.Current.MainPage.DisplayAlert("Uwaga", $"Poziom cukru ({sugarValue}) jest zbyt niski (min {markerPrefs.MinValue})", "Ok");
            }

            else if (sugar > markerPrefs.MaxValue)
                await Application.Current.MainPage.DisplayAlert("Uwaga", $"Poziom cukru ({sugarValue}) jest zbyt wysoki (max {markerPrefs.MaxValue})", "Ok");

            DateTime fullDate = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);

            var dto = new SugarEntryDto
            {
                SugarValue = sugar,
                MealTime = fullDate,
                MealType = SelectedMealType,
                MealMarker = SelectedMealMarker
            };

            try
            {
                await _apiService.AddSugarEntryAsync(dto);
                await Shell.Current.DisplayAlert("Sukces", "Pomiar został wysłany", "OK");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Błąd przy wysyłaniu: {ex.Message}";
            }
        }
    }
}
