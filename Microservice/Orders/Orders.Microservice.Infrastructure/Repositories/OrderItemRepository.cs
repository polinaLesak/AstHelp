using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;

namespace Orders.Microservice.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem, Guid>, IOrderItemRepository
    {
        public OrderItemRepository(EFDBContext context)
            : base(context) { }

        public async Task<List<OrderItem>> GetAllOrderItemsByCatalogId(int catalogId)
        {
            return await _context.OrderItems
                .Include(x => x.Order)
                .Where(x => x.CatalogId == catalogId)
                .ToListAsync();
        }

        public async Task<List<OrderItem>> GetAllOrderItemsByProductId(Guid productId)
        {
            return await _context.OrderItems
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}
