using MediatR;
using Notification.Microservice.Domain.Repositories;
using Notifications.Microservice.Application.Queries;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.Application.Handlers
{
    public class GetAllNotificationsByUserIdQueryHandler
        : IRequestHandler<GetAllNotificationsByUserIdQuery, IEnumerable<NotificationEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllNotificationsByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NotificationEntity>> Handle(
            GetAllNotificationsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.Notification.GetAllAsync();
        }
    }
}
