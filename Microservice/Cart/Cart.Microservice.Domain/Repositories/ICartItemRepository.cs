namespace Cart.Microservice.Domain.Repositories
{
    public interface ICartItemRepository : IGenericRepository<Entities.CartItem, Guid>
    {
        Task<int> GetCartProductsCountByUserId(int userId);
    }
}
