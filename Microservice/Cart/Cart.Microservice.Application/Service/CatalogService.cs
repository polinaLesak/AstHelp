using Cart.Microservice.Application.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Cart.Microservice.Application.Service
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly MachineTokenService _machineTokenService;

        public CatalogService()
        {
        }

        public CatalogService(HttpClient httpClient, MachineTokenService machineTokenService)
        {
            _httpClient = httpClient;
            _machineTokenService = machineTokenService;
        }

        public virtual async Task<ProductInfoDto> GetProductInfoAsync(Guid productId)
        {
            var token = await _machineTokenService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/Product/{productId}");

            if (response.IsSuccessStatusCode)
            {
                using (var document = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync()))
                {
                    var root = document.RootElement;

                    return new ProductInfoDto()
                    {
                        CatalogId = root.GetProperty("catalogId").GetInt32(),
                        ProductId = root.GetProperty("id").GetGuid(),
                        CatalogName = root.GetProperty("catalog").GetProperty("name").GetString(),
                        ProductName = root.GetProperty("name").GetString()
                    };
                }
            }
            else
                throw new Exception("Ошибка получения информации о продукте из микросервиса Catalog.");
        }
    }
}
