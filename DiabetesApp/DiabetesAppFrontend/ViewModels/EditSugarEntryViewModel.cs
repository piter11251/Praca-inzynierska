using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class EditSugarEntryViewModel: ObservableObject, IQueryAttributable
    {
        private readonly DemoApiClientService _apiService;
        private SugarEntry _sugarEntry;

        public EditSugarEntryViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
            MealMarkers = Enum.GetValues(typeof(MealMarker)).Cast<MealMarker>().ToList();

            SaveCommand = new AsyncRelayCommand(SaveAsync);
            CancelCommand = new AsyncRelayCommand(CancelAsync);
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.TryGetValue("Entry", out var entry) && entry is SugarEntry sugarEntry)
            {
                _sugarEntry = sugarEntry;
                SugarValue = _sugarEntry.SugarValue.ToString();
                MealDate = _sugarEntry.MealTime.Date;
                MealTime = _sugarEntry.MealTime.TimeOfDay;
                SelectedMealMarker = Enum.TryParse(_sugarEntry.MealMarker, out MealMarker marker) ? marker : MealMarker.Before_Meal;
            }
        }

        [ObservableProperty] private string sugarValue;
        [ObservableProperty] private DateTime mealDate;
        [ObservableProperty] private TimeSpan mealTime;
        [ObservableProperty] private MealMarker selectedMealMarker;
        public List<MealMarker> MealMarkers { get; }
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand CancelCommand { get; }

        private async Task SaveAsync()
        {
            _sugarEntry.SugarValue = int.TryParse(SugarValue, out int newValue) ? newValue : _sugarEntry.SugarValue;
            _sugarEntry.MealTime = MealDate + MealTime;
            _sugarEntry.MealMarker = SelectedMealMarker.ToString();

            if (await _apiService.ModifyEntryAsync(_sugarEntry))
                await Shell.Current.GoToAsync("..");
            else
                await Shell.Current.DisplayAlert("Błąd", "Nie udało się zapisać zmian", "Ok");
        }

        private async Task CancelAsync() => await Shell.Current.GoToAsync("..");
    }
}
