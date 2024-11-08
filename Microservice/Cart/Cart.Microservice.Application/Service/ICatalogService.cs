using Cart.Microservice.Application.DTOs;

namespace Cart.Microservice.Application.Service
{
    public interface ICatalogService
    {
        Task<ProductInfoDto> GetProductInfoAsync(Guid productId);
    }
}
