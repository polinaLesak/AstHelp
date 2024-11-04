using Cart.Microservice.Application.Events;
using Cart.Microservice.Domain.Repositories;
using MediatR;

namespace Cart.Microservice.Application.EventsHandlers
{
    public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDeletedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            // Логика для обработки удаления пользователя
        }
    }
}
