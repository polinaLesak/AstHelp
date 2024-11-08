using Orders.Microservice.Application.DTOs;

namespace Orders.Microservice.Application.Service
{
    public interface ICatalogService
    {
        Task<ProductInfoDto> GetProductInfoAsync(Guid productId);
        Task<List<ProductInfoDto>> GetProductsInfoAsync(Guid[] productIds);
    }
}
