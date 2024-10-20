using Microsoft.Extensions.Configuration;
using Orders.Microservice.Application.DTOs;
using System.Net.Http.Json;

namespace Orders.Microservice.Application.Service
{
    public class MachineTokenService : IMachineTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _cachedToken;
        private DateTime _tokenExpiration;

        public MachineTokenService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync()
        {
            if (_cachedToken != null && _tokenExpiration > DateTime.UtcNow)
            {
                return _cachedToken;
            }

            var tokenRequest = new { ApiKey = _configuration["MachineApiKey"] };

            var response = await _httpClient.PostAsJsonAsync("api/MachineAuth/Token", tokenRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                _cachedToken = result.Token;
                _tokenExpiration = DateTime.UtcNow.AddMinutes(30);
                return _cachedToken;
            }

            throw new Exception("Unable to get token from Identity service.");
        }
    }
}
