using Cart.Microservice.Infrastructure.Persistence;
using Cart.Microservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Repository
{
    public class GenericRepositoryTests
    {
        private readonly EFDBContext _context;
        private readonly GenericRepository<CartEntity, Guid> _repository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EFDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new EFDBContext(options);
            _repository = new GenericRepository<CartEntity, Guid>(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            var entity = new CartEntity { Id = Guid.NewGuid(), UserId = 1, Items = [] };

            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.NotNull(result.First());
            Assert.Equal(entity.UserId, result.First().UserId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntityById()
        {
            var id = Guid.NewGuid();
            var entity = new CartEntity { Id = id, UserId = 1, Items = [] };

            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(entity.UserId, result.UserId);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            var entities = new List<CartEntity>
            {
                new() { Id = Guid.NewGuid(), UserId = 1, Items = [] },
                new() { Id = Guid.NewGuid(), UserId = 1, Items = [] }
            };

            foreach (var entity in entities)
            {
                await _repository.AddAsync(entity);
            }
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task FindAsync_ShouldReturnFilteredEntities()
        {
            await _repository.AddAsync(new CartEntity { Id = Guid.NewGuid(), UserId = 1, Items = [] });
            await _repository.AddAsync(new CartEntity { Id = Guid.NewGuid(), UserId = 2, Items = [] });
            await _context.SaveChangesAsync();

            var result = await _repository.FindAsync(e => e.UserId == 2);

            Assert.Single(result);
            Assert.NotNull(result.First());
            var entity = result.First();
            Assert.Equal(2, entity.UserId);
        }

        [Fact]
        public async Task Update_ShouldModifyEntity()
        {
            var id = Guid.NewGuid();
            var entity = new CartEntity { Id = id, UserId = 1, Items = [] };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            entity.UserId = 2;
            _repository.Update(entity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _repository.GetByIdAsync(id);
            Assert.Equal(2, updatedEntity.UserId);
        }

        [Fact]
        public async Task Remove_ShouldDeleteEntity()
        {
            var id = Guid.NewGuid();
            var entity = new CartEntity { Id = id, UserId = 1, Items = [] };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            _repository.Remove(entity);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(id);
            Assert.Null(result);
        }
    }
}
