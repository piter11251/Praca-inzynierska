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
        public EditSugarEntryViewModel(SugarEntry sugarEntry, DemoApiClientService apiService)
        {
            Entry = sugarEntry;
            _apiService = apiService;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if(Entry == null) return;
            try
            {
                await _apiService.ModifyEntryAsync(entry);
                WeakReferenceMessenger.Default.Send(new SugarEntryUpdatedMessage(Entry));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }
    }
}
