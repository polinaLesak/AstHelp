using MediatR;
using Orders.Microservice.Application.Events;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.EventsHandlers
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
            var cartItems = await _unitOfWork.OrderItems.GetAllOrderItemsByCatalogId(catalog.CatalogId);
            foreach (var item in cartItems)
            {
                item.CatalogName = catalog.CatalogName;
                _unitOfWork.OrderItems.Update(item);
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
