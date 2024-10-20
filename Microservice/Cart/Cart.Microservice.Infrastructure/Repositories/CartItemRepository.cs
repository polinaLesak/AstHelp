using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using Cart.Microservice.Infrastructure.Persistence;

namespace Cart.Microservice.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem, Guid>, ICartItemRepository
    {
        public CartItemRepository(EFDBContext context)
            : base(context) { }

    }
}
