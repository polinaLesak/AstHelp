using Cart.Microservice.Domain.Entities;

namespace Cart.Microservice.Domain.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem, Guid>
    {
        Task<int> GetCartProductsCountByUserId(int userId);
        Task<List<CartItem>> GetAllCartItemsByProductId(Guid productId);
        Task<List<CartItem>> GetAllCartItemsByCatalogId(int catalogId);
    }
}
