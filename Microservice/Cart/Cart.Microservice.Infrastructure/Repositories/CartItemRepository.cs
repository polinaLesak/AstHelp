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

        public async Task<List<CartItem>> GetAllCartItemsByCatalogId(int catalogId)
        {
            return await _context.CartItems
                .Where(x => x.CatalogId == catalogId)
                .ToListAsync();
        }

        public async Task<List<CartItem>> GetAllCartItemsByProductId(Guid productId)
        {
            return await _context.CartItems
                .Include(x => x.Cart)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<int> GetCartProductsCountByUserId(int userId)
        {
            return await _context.CartItems.AsNoTracking()
                .Where(x => x.Cart.UserId == userId)
                .CountAsync();
        }
    }
}
