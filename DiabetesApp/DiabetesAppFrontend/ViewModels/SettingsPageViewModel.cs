using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.CaseMap;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private readonly DemoApiClientService _apiService;
        [ObservableProperty]
        private ObservableCollection<PreferableSugarLevelModel> sugarLevels;

        public SettingsPageViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
            SugarLevels = new ObservableCollection<PreferableSugarLevelModel>();
        }

        public async Task LoadPreferencesAsync()
        {
            var data = await _apiService.GetUserPreferencesAsync();
            if (data == null)
            {
                return;
            }
            SugarLevels.Clear();
            foreach(var lvl in data.PrefelableSugarLevels)
            {
                var enumValue = (MealMarker)Enum.Parse(typeof(MealMarker), lvl.MealMarker);
                SugarLevels.Add(new PreferableSugarLevelModel
                {
                    Id = lvl.Id,
                    MealMarker = MealMarkerEnum.FriendlyNames(enumValue),
                    MinValue = lvl.MinValue,
                    MaxValue = lvl.MaxValue,
                });
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var dto = new UpdateUserPreferencesDto
            {
                PreferableSugarLevels = SugarLevels.Select(x => new UpdateSugarLevelDto
                {
                    Id = x.Id,
                    MinValue = x.MinValue,
                    MaxValue = x.MaxValue
                })
                .ToList()
            };

            var success = await _apiService.UpdateUserPreferencesAsync(dto);
            if(!success)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać preferencji", "Ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Sukces", "Preferencje zostaly zapisane", "Ok");
            }
        }
    }

    public class PreferableSugarLevelModel
    {
        public int Id { get; set; }
        public string MealMarker { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
