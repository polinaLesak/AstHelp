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

        public async Task Handle(UserDeletedEvent userDeletedEvent, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userDeletedEvent.UserId);
            if (cart != null)
            {
                _unitOfWork.Carts.Remove(cart);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
