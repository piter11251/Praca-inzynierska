using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5265/api/")
            };
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Email i hasło są wymagane");
            }

            var loginData = new
            {
                Email = email,
                Password = password
            };

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("account/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Błąd logowania: {errorMessage}"); // Dodanie logowania błędu
                    throw new Exception($"Błąd logowania: {errorMessage}");
                }

                var token = await response.Content.ReadAsStringAsync();
                Preferences.Set("AuthToken", token);
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia: {ex.Message}"); // Dodanie logowania
                throw new Exception($"Błąd połączenia: {ex.Message}", ex); // Rzucenie wyjątku z pełnymi danymi
            }
        }
    }
}
