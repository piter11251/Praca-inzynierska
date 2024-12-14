using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5265/api/")
            };
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var token = Preferences.Get("AuthToken", string.Empty);
            if(!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.SendAsync(request);
        }

        public async Task<string> GetDataAsync(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await SendRequestAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Przetwarzaj odpowiedź
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                // Obsługuje błędy
                throw new Exception("Błąd podczas pobierania danych");
            }
        }
    }
}
