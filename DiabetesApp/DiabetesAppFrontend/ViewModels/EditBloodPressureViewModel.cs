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
    public partial class EditBloodPressureViewModel: ObservableObject
    {
        private readonly DemoApiClientService _apiService;
        [ObservableProperty] private int id;
        [ObservableProperty] private int stolicPressure;
        [ObservableProperty] private int diastolicPressure;
        [ObservableProperty] private bool isIrregularPulse;
        [ObservableProperty] private int pulse;
        [ObservableProperty] private DateTime measureDate;

        public EditBloodPressureViewModel(BloodPressureDto entry, DemoApiClientService apiService)
        {
            _apiService = apiService;
            Id = entry.Id;
            MeasureDate = entry.MeasurementDate;
            StolicPressure = entry.StolicPressure;
            DiastolicPressure = entry.DiastolicPressure;
            Pulse = entry.Pulse;
            IsIrregularPulse = entry.IsIrregularPulse;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            try
            {
                var patchEntry = new BloodPressureDto
                {
                    Id = this.id,
                    StolicPressure = this.StolicPressure,
                    DiastolicPressure = this.DiastolicPressure,
                    Pulse = this.Pulse,
                    MeasurementDate = this.MeasureDate,
                    IsIrregularPulse = this.IsIrregularPulse
                };
                bool success = await _apiService.ModifyBloodPressureAsync(patchEntry);
                if (!success)
                {
                    Console.WriteLine("[ERROR] NIE UDALO SIE");
                }
                else
                {
                    Console.WriteLine("[DEBUG] UDALO SIE");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
