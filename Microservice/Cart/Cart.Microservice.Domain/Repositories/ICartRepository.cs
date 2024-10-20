namespace Cart.Microservice.Domain.Repositories
{
    public interface ICartRepository : IGenericRepository<Entities.Cart, Guid>
    {
        Task<Entities.Cart> GetCartByUserIdAsync(int userId);
    }
}
