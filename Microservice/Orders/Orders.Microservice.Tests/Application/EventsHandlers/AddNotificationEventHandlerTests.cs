// using Moq;
// using Notification.Microservice.Application.EventsHandlers;
// using Notification.Microservice.Domain.Repositories;
// using Notification.Microservice.Infrastructure.Messaging.Events;
// using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;
// using NotificationEntityType = Notification.Microservice.Domain.Entities.NotificationType;
//
// namespace Notification.Microservice.Tests.Application.EventsHandlers;
//
// public class AddNotificationEventHandlerTests
// {
//     [Fact]
//     public async Task Handle_ShouldAddNotification_AndCommit()
//     {
//         var userId = 1;
//         var title = "Test Title";
//         var message = "Test Message";
//         var type = NotificationEntityType.Info;
//         
//         var addNotificationEvent = new AddNotificationEvent
//         {
//             UserId = userId,
//             Title = title,
//             Message = message,
//             Type = type
//         };
//
//         var mockUnitOfWork = new Mock<IUnitOfWork>();
//         var mockNotificationRepository = new Mock<INotificationRepository>();
//
//         mockUnitOfWork.Setup(uow => uow.Notification).Returns(mockNotificationRepository.Object);
//
//         var handler = new AddNotificationEventHandler(mockUnitOfWork.Object);
//
//         await handler.Handle(addNotificationEvent, CancellationToken.None);
//
//         mockNotificationRepository.Verify(repo => repo.AddAsync(It.Is<NotificationEntity>(n =>
//             n.UserId == userId &&
//             n.Title == title &&
//             n.Message == message &&
//             n.Type == type &&
//             n.Timestamp != DateTime.MinValue
//         )), Times.Once);
//
//         mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
//     }
// }