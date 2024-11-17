using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Domain.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order, Guid>
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersByManagerIdAsync(int managerId);
        Task<int> CountByManagerIdAsync(int managerId);
    }
}
