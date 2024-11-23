using MediatR;
using Orders.Microservice.Application.Events;
using Orders.Microservice.Domain.Repositories;

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
            var cart = await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(userDeletedEvent.UserId);
            foreach (var item in cart)
            {
                if (item != null)
                {
                    _unitOfWork.Orders.Remove(item);
                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
