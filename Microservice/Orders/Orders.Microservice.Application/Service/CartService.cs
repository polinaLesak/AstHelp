using System.Net.Http.Headers;

namespace Orders.Microservice.Application.Service
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly MachineTokenService _machineTokenService;

        public CartService(HttpClient httpClient, MachineTokenService machineTokenService)
        {
            _httpClient = httpClient;
            _machineTokenService = machineTokenService;
        }

        public async Task ResetCartByUserId(int userId)
        {
            var token = await _machineTokenService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync($"api/Cart/Clear?userId={userId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Ошибка получения информации о продукте из микросервиса Catalog.");
        }
    }
}
