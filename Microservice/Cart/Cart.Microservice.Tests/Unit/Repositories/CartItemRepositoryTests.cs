using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Infrastructure.Persistence;
using Cart.Microservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Repository
{
    public class CartItemRepositoryTests
    {
        private readonly EFDBContext _context;
        private readonly CartItemRepository _repository;

        public CartItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EFDBContext>()
                .UseInMemoryDatabase(databaseName: "NotificationDB")
                .Options;

            _context = new EFDBContext(options);
            _repository = new CartItemRepository(_context);
        }

        [Fact]
        public async Task GetAllCartItemsByCatalogId_ShouldReturnItems_WhenItemsExist()
        {
            var catalogId = 1;
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), CatalogId = catalogId });
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), CatalogId = catalogId });
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), CatalogId = 2 });
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllCartItemsByCatalogId(catalogId);

            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.Equal(catalogId, item.CatalogId));
        }

        [Fact]
        public async Task GetCartProductsCountByUserId_ShouldReturnCorrectCount()
        {
            var userId = 1;
            var cart = new CartEntity { Id = Guid.NewGuid(), UserId = userId };
            _context.Carts.Add(cart);
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), CartId = cart.Id, Cart = cart });
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), CartId = cart.Id, Cart = cart });
            await _context.SaveChangesAsync();

            var result = await _repository.GetCartProductsCountByUserId(userId);

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetAllCartItemsByProductId_ShouldReturnItems_WhenItemsExist()
        {
            var productId = Guid.NewGuid();
            var cart = new CartEntity { Id = Guid.NewGuid(), UserId = 1 };
            _context.Carts.Add(cart);
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), ProductId = productId, CartId = cart.Id });
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), ProductId = productId, CartId = cart.Id });
            _context.CartItems.Add(new CartItem { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), CartId = cart.Id });
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllCartItemsByProductId(productId);

            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.Equal(productId, item.ProductId));
        }
    }
}
