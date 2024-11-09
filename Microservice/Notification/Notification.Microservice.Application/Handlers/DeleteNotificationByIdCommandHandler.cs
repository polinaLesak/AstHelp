using MediatR;
using Notification.Microservice.Application.Exceptions;
using Notification.Microservice.Domain.Repositories;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.Handlers
{
    public class DeleteNotificationByIdCommandHandler
        : IRequestHandler<DeleteNotificationByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotificationByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            DeleteNotificationByIdCommand request,
            CancellationToken cancellationToken)
        {
            NotificationEntity entity = await _unitOfWork.Notification.GetByIdAsync(request.NotificationId);
            if (entity == null)
                throw new NotFoundException($"Уведомление с ID {request.NotificationId} не найдено.");
            entity.IsRead = true;
            _unitOfWork.Notification.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
