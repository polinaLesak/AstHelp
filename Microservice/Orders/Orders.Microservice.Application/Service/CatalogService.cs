using Orders.Microservice.Application.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Orders.Microservice.Application.Service
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly MachineTokenService _machineTokenService;

        public CatalogService(HttpClient httpClient, MachineTokenService machineTokenService)
        {
            _httpClient = httpClient;
            _machineTokenService = machineTokenService;
        }

        public async Task<ProductInfoDto> GetProductInfoAsync(Guid productId)
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

        public async Task<List<ProductInfoDto>> GetProductsInfoAsync(Guid[] productIds)
        {
            var token = await _machineTokenService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/Product/Products?ids={string.Join("&ids=", productIds)}");

            if (response.IsSuccessStatusCode)
            {
                using (var document = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync()))
                {
                    var root = document.RootElement;
                    var productDtos = new List<ProductInfoDto>();

                    if (root.ValueKind != JsonValueKind.Array)
                    {
                        throw new Exception("Ожидался JSON массив продуктов.");
                    }

                    foreach (var element in root.EnumerateArray())
                    {
                        var productDto = new ProductInfoDto
                        {
                            ProductId = element.GetProperty("id").GetGuid(),
                            ProductName = element.GetProperty("name").GetString(),
                            CatalogId = element.GetProperty("catalogId").GetInt32(),
                            CatalogName = element.GetProperty("catalog")
                                               .GetProperty("name")
                                               .GetString()
                        };

                        productDtos.Add(productDto);
                    }

                    return productDtos;
                }
            }
            else
                throw new Exception("Ошибка получения информации о продукте из микросервиса Catalog.");
        }
    }
}
