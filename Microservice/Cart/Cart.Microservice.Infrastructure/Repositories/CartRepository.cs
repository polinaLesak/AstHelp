using Cart.Microservice.Domain.Repositories;
using Cart.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cart.Microservice.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Domain.Entities.Cart, Guid>, ICartRepository
    {
        public CartRepository(EFDBContext context)
            : base(context) { }

        public async Task<Domain.Entities.Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
