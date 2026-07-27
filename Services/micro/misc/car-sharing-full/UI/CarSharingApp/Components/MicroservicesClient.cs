using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CarSharingApp.Components
{
    public class MicroservicesClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MicroservicesClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> CallMicroserviceAsync(string serviceName, string endpoint)
        {
            var baseUrl = _configuration[$"Microservices:{serviceName}"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new System.InvalidOperationException($"Base URL for {serviceName} is not configured.");
            }

            var response = await _httpClient.GetAsync($"{baseUrl}/{endpoint}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
