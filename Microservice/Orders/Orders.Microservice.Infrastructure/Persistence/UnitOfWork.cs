using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;

        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }

        public UnitOfWork(
            EFDBContext context,
            IOrderRepository orders,
            IOrderItemRepository orderItems)
        {
            _context = context;
            Orders = orders;
            OrderItems = orderItems;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
