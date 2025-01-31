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
    public partial class SugarEntryViewModel: ObservableObject
    {
        private readonly DemoApiClientService _apiService;

        [ObservableProperty] private string sugarValue;
        [ObservableProperty] private DateTime date = DateTime.Now;
        [ObservableProperty] private TimeSpan time = DateTime.Now.TimeOfDay;
        [ObservableProperty] private int selectedMealType;
        [ObservableProperty] private int selectedMealMarker;
        [ObservableProperty] private string errorMessage;

        public SugarEntryViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
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
