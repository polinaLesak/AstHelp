using Cart.Microservice.Domain.Repositories;

namespace Cart.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;

        public ICartRepository Carts { get; }
        public ICartItemRepository CartItems { get; }

        public UnitOfWork(
            EFDBContext context,
            ICartRepository carts,
            ICartItemRepository cartItems)
        {
            _context = context;
            Carts = carts;
            CartItems = cartItems;
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
