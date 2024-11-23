using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Domain.Repositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem, Guid>
    {
        Task<List<OrderItem>> GetAllOrderItemsByProductId(Guid productId);
        Task<List<OrderItem>> GetAllOrderItemsByCatalogId(int catalogId);
    }
}
