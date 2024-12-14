using Demo.ApiClient.Models;
using Demo.ApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
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
