using Demo.ApiClient.Models.ApiModels;
using Demo.ApiClient;
using DiabetesAppFrontend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Services
{
    public class SugarDataService
    {
        private readonly DemoApiClientService _apiService;

        public SugarDataService(DemoApiClientService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<SugarEntry>> GetAllSugarEntries()
        {
            return await _apiService.GetAllSugarEntries();
        }
    }
}