using Microsoft.EntityFrameworkCore;
using Notification.Microservice.Infrastructure.Persistence;
using Notification.Microservice.Infrastructure.Repositories;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Tests.Infrastructure.Repositories;

public class NotificationRepositoryTests
{
    private readonly EFDBContext _context;
    private readonly NotificationRepository _repository;

    public NotificationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<EFDBContext>()
            .UseInMemoryDatabase(databaseName: "NotificationDB")
            .Options;

        _context = new EFDBContext(options);
        _repository = new NotificationRepository(_context);
    }

    [Fact]
    public async Task GetAllNotificationsByUserId_ShouldReturnUserNotifications()
    {
        var notifications = new List<NotificationEntity>
        {
            new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test1" },
            new NotificationEntity { Id = Guid.NewGuid(), UserId = 1, Message = "Test2" },
            new NotificationEntity { Id = Guid.NewGuid(), UserId = 2, Message = "Test3" }
        };

        await _context.Notifications.AddRangeAsync(notifications);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllNotificationsByUserId(1);

        Assert.Equal(2, result.Count);
        Assert.All(result, n => Assert.Equal(1, n.UserId));
    }
}