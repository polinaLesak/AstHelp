using Microsoft.EntityFrameworkCore;
using Moq;
using Notification.Microservice.Domain.Repositories;
using Notification.Microservice.Infrastructure.Persistence;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Unit.Repository
{
    public class UnitOfWorkTests
    {
        private readonly EFDBContext _context;
        private readonly Mock<INotificationRepository> _mockNotificationRepo;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<EFDBContext>()
                .UseInMemoryDatabase(databaseName: "UnitOfWorkTestDB")
                .Options;

            _context = new EFDBContext(options);
            _mockNotificationRepo = new Mock<INotificationRepository>();

            _unitOfWork = new UnitOfWork(_context, _mockNotificationRepo.Object);
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
            var notification = new NotificationEntity
            {
                Id = Guid.NewGuid(),
                UserId = 1,
                Title = "Тестовое умедомление",
                Message = "Это тестовое уведомление",
                Timestamp = DateTime.UtcNow
            };
            _context.Entry(notification).State = EntityState.Added;
            await _unitOfWork.Notification.AddAsync(notification);

            var result = await _unitOfWork.CommitAsync();

            Assert.Equal(1, result);
        }

        [Fact]
        public void Dispose_ShouldCallDisposeOnContext()
        {
            _unitOfWork.Dispose();

            Assert.Throws<ObjectDisposedException>(() => _context.Notifications.ToList());
        }
    }
}
