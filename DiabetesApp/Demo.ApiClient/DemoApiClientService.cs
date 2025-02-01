using Demo.ApiClient.Models;
using Demo.ApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Demo.ApiClient
{
    public class DemoApiClientService
    {
        private readonly HttpClient _httpClient;
        public DemoApiClientService(ApiClientOptions apiClientOptions)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(apiClientOptions.BaseApiAddress);
        }

        public async Task<List<EntryDto>?> GetAllEntriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EntryDto>?>("/api/sugar-entries");

        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await SetAuthTokenAsync(token);
                return token;
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Nieudane logowanie: {errorMessage}");
        }

        private async Task SetAuthTokenAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            await SecureStorage.SetAsync("auth_token", token);

        }

        public async Task<string?> GetAuthTokenAsync()
        {
            return await SecureStorage.GetAsync("auth_token");
        }

        public async Task InitializeAuthTokenAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/register", dto);
            if(!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Błąd rejestracji: {errorMessage}");
            }
        }

        public async Task AddSugarEntryAsync(SugarEntryDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/sugar-entries/create-entry", dto);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Nie udalo sie przeslac pomiaru: {errorMessage}");
            }
        }

        public async Task AddBloodPressureAsync(BloodPressureDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/pressure-entries/create-pressure", dto);
            if(!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Nie udalo sie przeslac pomiaru: {errorMessage}");
            }
        }

        public async Task<List<SugarEntry>> GetAllSugarEntries(int days)
        {
            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                if (string.IsNullOrEmpty(token))
                {
                    return new List<SugarEntry>();
                }

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"api/sugar-entries?days={days}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<SugarEntry>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result ?? new List<SugarEntry>();


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<SugarEntry>();
            }
        }
        public async Task<List<BloodPressureDto>> GetBloodPressureEntries(int days)
        {
            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                if (string.IsNullOrEmpty(token))
                {
                    return new List<BloodPressureDto>();
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"api/pressure-entries?days={days}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[DEBUG] Surowy json z API: {json}");
                var result = JsonSerializer.Deserialize<List<BloodPressureDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if(result == null)
                {
                    Console.WriteLine("[ERROR] Deserializacja zwrocila null");
                    return new List<BloodPressureDto>();
                }
                foreach(var bp in result)
                {
                    Console.WriteLine($"[DEBUG] Otrzymany wpis: {bp.MeasurementDate}, {bp.StolicPressure}/{bp.DiastolicPressure}");
                }

                return result;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                return new List<BloodPressureDto>();
            }
        }

        public async Task<bool> ModifyEntryAsync(SugarEntry entry)
        {
            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var dto = new ModifyEntryDto
                {
                    SugarValue = entry.SugarValue,
                    MealTime = entry.MealTime,
                    MealMarker = entry.MealMarker
                };

                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/sugar-entries/{entry.Id}")
                {
                    Content = content
                };
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> ModifyBloodPressureAsync(BloodPressureEntry entry)
        {
            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var dto = new BloodPressureEntry
                {
                    StolicValue = entry.StolicValue,
                    DiastolicValue = entry.DiastolicValue,
                    MeasureTime = entry.MeasureTime
                };

                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/pressure-entries/{entry.Id}")
                {
                    Content = content
                };
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /*public async Task<Product?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product?>($"/api/Product/{id}");
        }

        public async Task SaveProduct(Product product)
        {
            await _httpClient.PostAsJsonAsync("api/Product", product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _httpClient.PutAsJsonAsync("api/Product", product);
        }

        public async Task DeleteProduct(int id)
        {
            await _httpClient.DeleteAsync($"/api/Product/{id}");
        }*/
    }
}
