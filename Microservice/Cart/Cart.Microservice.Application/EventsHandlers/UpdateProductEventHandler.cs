using Cart.Microservice.Application.Events;
using Cart.Microservice.Domain.Repositories;
using Cart.Microservice.Infrastructure.Messaging;
using Cart.Microservice.Infrastructure.Messaging.Events;
using MediatR;

namespace Notificationt.Microservice.Application.EventsHandlers
{
    public class UpdateProductEventHandler : INotificationHandler<UpdateProductEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public UpdateProductEventHandler(IUnitOfWork unitOfWork, RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task Handle(UpdateProductEvent product, CancellationToken cancellationToken)
        {
            var cartItems = await _unitOfWork.CartItems.GetAllCartItemsByProductId(product.ProductId);
            foreach (var item in cartItems)
            {
                if (item.Quantity > product.Quantity)
                {
                    _unitOfWork.CartItems.Remove(item);

                    await _unitOfWork.CommitAsync();

                    _rabbitMQProducer.Publish(new AddNotificationEvent
                    {
                        UserId = item.Cart.UserId,
                        Title = "Удаление товара из корзины",
                        Message = $"Обратите внимание! Продукт {item.CatalogName} {item.ProductName} был удалён " +
                            $"из корзины из-за отсутствия его в нужном количестве",
                        Type = NotificationType.Warning
                    });
                }
                else
                {
                    item.ProductName = product.ProductName;
                    item.CatalogId = product.CatalogId;
                    item.CatalogName = product.CatalogName;
                    _unitOfWork.CartItems.Update(item);

                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
