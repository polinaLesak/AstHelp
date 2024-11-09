using MediatR;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.Handlers
{
    public class AddNotificationCommandHandler
        : IRequestHandler<AddNotificationCommand, NotificationEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationEntity> Handle(
            AddNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = new NotificationEntity
            {
                UserId = request.UserId,
                Title = request.Title,
                Message = request.Message,
                Timestamp = DateTime.UtcNow,
                Type = request.Type
            };
            await _unitOfWork.Notification.AddAsync(notification);
            await _unitOfWork.CommitAsync();

            return notification;
        }
    }
}
