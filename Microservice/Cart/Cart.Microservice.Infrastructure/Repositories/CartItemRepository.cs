using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using Cart.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cart.Microservice.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem, Guid>, ICartItemRepository
    {
        public CartItemRepository(EFDBContext context)
            : base(context) { }

        public async Task<int> GetCartProductsCountByUserId(int userId)
        {
            return await _context.CartItems.AsNoTracking()
                .Where(x => x.Cart.UserId == userId)
                .CountAsync();
        }
    }
}
