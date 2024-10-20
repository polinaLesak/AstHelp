using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;

namespace Orders.Microservice.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem, Guid>, IOrderItemRepository
    {
        public OrderItemRepository(EFDBContext context)
            : base(context) { }

    }
}
