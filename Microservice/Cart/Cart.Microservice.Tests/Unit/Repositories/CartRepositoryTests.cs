using Cart.Microservice.Infrastructure.Persistence;
using Cart.Microservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Repository
{
    public class CartRepositoryTests
    {
        private readonly EFDBContext _context;
        private readonly CartRepository _repository;

        public CartRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EFDBContext>()
                .UseInMemoryDatabase(databaseName: "NotificationDB")
                .Options;

            _context = new EFDBContext(options);
            _repository = new CartRepository(_context);
        }

        [Fact]
        public async Task GetCartByUserIdAsync_ShouldReturnCart_WhenCartExists()
        {
            var userId = 1;
            var cart = new CartEntity { Id = Guid.NewGuid(), UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCartByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task GetCartByUserIdAsync_ShouldReturnNull_WhenCartDoesNotExist()
        {
            var notifications = new List<CartEntity>
            {
                new() { Id = Guid.NewGuid(), UserId = 1 },
                new() { Id = Guid.NewGuid(), UserId = 22 },
                new() { Id = Guid.NewGuid(), UserId = 33 }
            };

            await _context.Carts.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCartByUserIdAsync(99);

            Assert.Null(result);
        }
    }
}
