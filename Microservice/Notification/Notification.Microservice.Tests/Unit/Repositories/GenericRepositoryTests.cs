using Microsoft.EntityFrameworkCore;
using Notification.Microservice.Infrastructure.Persistence;
using Notification.Microservice.Infrastructure.Repositories;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Unit.Repository
{
    public class GenericRepositoryTests
    {
        private readonly EFDBContext _context;
        private readonly GenericRepository<NotificationEntity, Guid> _repository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EFDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new EFDBContext(options);
            _repository = new GenericRepository<NotificationEntity, Guid>(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            var entity = new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test" };

            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.NotNull(result.First());
            Assert.Equal(entity.Message, result.First().Message);
            Assert.Equal(entity.UserId, result.First().UserId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntityById()
        {
            var id = Guid.NewGuid();
            var entity = new NotificationEntity { Id = id, UserId = 1, Message = "Test" };

            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(entity.Message, result.Message);
            Assert.Equal(entity.UserId, result.UserId);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            var entities = new List<NotificationEntity>
            {
                new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test1" },
                new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test2" }
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
            await _repository.AddAsync(new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test1" });
            await _repository.AddAsync(new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test2" });
            await _context.SaveChangesAsync();

            var result = await _repository.FindAsync(e => e.Message.Contains("Test2"));

            Assert.Single(result);
            Assert.NotNull(result.First());
            var entity = result.First();
            Assert.Equal(1, entity.UserId);
            Assert.Equal("Test2", entity.Message);
        }

        [Fact]
        public async Task Update_ShouldModifyEntity()
        {
            var id = Guid.NewGuid();
            var entity = new NotificationEntity { Id = id, UserId = 1, Message = "Test" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            entity.Message = "TestUpdated";
            _repository.Update(entity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _repository.GetByIdAsync(id);
            Assert.Equal("TestUpdated", updatedEntity.Message);
        }

        [Fact]
        public async Task Remove_ShouldDeleteEntity()
        {
            var id = Guid.NewGuid();
            var entity = new NotificationEntity { Id = id, UserId = 1, Message = "Test" };
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            _repository.Remove(entity);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(id);
            Assert.Null(result);
        }
    }
}
