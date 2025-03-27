using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    public partial class EditSugarEntryViewModel : ObservableObject
    {
        private readonly DemoApiClientService _apiService;
        [ObservableProperty]
        private SugarEntry entry;
        [ObservableProperty]
        private string selectedMealMarker;
        [ObservableProperty]
        private DateTime mealDate;
        [ObservableProperty]
        private TimeSpan mealTime;
        public List<string> MealMarkerOptions { get; } = MealMarkerEnum.Values;
        public EditSugarEntryViewModel(SugarEntry sugarEntry, DemoApiClientService apiService)
        {
            Entry = sugarEntry;
            _apiService = apiService;

            MealDate = Entry.MealTime.Date;
            MealTime = Entry.MealTime.TimeOfDay;

            SelectedMealMarker = MealMarkerEnum.FriendlyNames(Enum.Parse<MealMarker>(Entry.MealMarker));
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if(Entry == null) return;
            try
            {
                Entry.MealTime = MealDate + MealTime;
                Entry.MealMarker = Enum.GetName(typeof(MealMarker), MealMarkerEnum.Values.IndexOf(SelectedMealMarker));
                await _apiService.ModifyEntryAsync(Entry);
                WeakReferenceMessenger.Default.Send(new SugarEntryUpdatedMessage(Entry));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }

    }
}
