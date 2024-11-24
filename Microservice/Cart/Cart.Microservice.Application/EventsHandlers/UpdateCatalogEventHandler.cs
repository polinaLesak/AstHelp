using Cart.Microservice.Application.Events;
using Cart.Microservice.Domain.Repositories;
using MediatR;

namespace Notificationt.Microservice.Application.EventsHandlers
{
    public class UpdateCatalogEventHandler : INotificationHandler<UpdateCatalogEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCatalogEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCatalogEvent catalog, CancellationToken cancellationToken)
        {
            var cartItems = await _unitOfWork.CartItems.GetAllCartItemsByCatalogId(catalog.CatalogId);
            foreach (var item in cartItems)
            {
                item.CatalogName = catalog.CatalogName;
                _unitOfWork.CartItems.Update(item);
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
