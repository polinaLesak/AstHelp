using Orders.Microservice.Application.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Orders.Microservice.Application.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly MachineTokenService _machineTokenService;

        public IdentityService(HttpClient httpClient, MachineTokenService machineTokenService)
        {
            _httpClient = httpClient;
            _machineTokenService = machineTokenService;
        }

        public async Task<List<UserDto>> GetAllManagersAsync()
        {
            var token = await _machineTokenService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/User/Managers");

            if (response.IsSuccessStatusCode)
            {
                var managers = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                return managers;
            }
            else
            {
                throw new Exception("Error retrieving managers from Identity service.");
            }
        }
    }
}
