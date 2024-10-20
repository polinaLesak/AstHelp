using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Domain.Repositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem, Guid>
    {

    }
}
