using Cart.Microservice.Domain.Repositories;
using Cart.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Repository
{
    public class UnitOfWorkTests
    {
        private readonly EFDBContext _context;
        private readonly Mock<ICartRepository> _mockRepo;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<EFDBContext>()
                .UseInMemoryDatabase(databaseName: "UnitOfWorkTestDB")
                .Options;

            _context = new EFDBContext(options);
            _mockRepo = new Mock<ICartRepository>();

            _unitOfWork = new UnitOfWork(_context, _mockRepo.Object, null);
        }

        [Fact]
        public async Task CommitAsyncWithoutSavingData_ShouldCallSaveChangesAsyncWithZeroResult()
        {
            var result = await _unitOfWork.CommitAsync();

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CommitAsyncWithSavingData_ShouldCallSaveChangesAsyncWithNonZeroResult()
        {
            var cart = new CartEntity
            {
                Id = Guid.NewGuid(),
                UserId = 1,
                Items = []
            };
            _context.Entry(cart).State = EntityState.Added;
            await _unitOfWork.Carts.AddAsync(cart);

            var result = await _unitOfWork.CommitAsync();

            Assert.Equal(1, result);
        }

        [Fact]
        public void Dispose_ShouldCallDisposeOnContext()
        {
            _unitOfWork.Dispose();

            Assert.Throws<ObjectDisposedException>(() => _context.Carts.ToList());
        }
    }
}
