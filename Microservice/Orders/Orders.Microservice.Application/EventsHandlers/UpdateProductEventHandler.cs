using MediatR;
using Orders.Microservice.Application.Events;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Messaging;
using Orders.Microservice.Infrastructure.Messaging.Events;

namespace Orders.Microservice.Application.EventsHandlers
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
            var cartItems = await _unitOfWork.OrderItems.GetAllOrderItemsByProductId(product.ProductId);
            foreach (var item in cartItems)
            {
                if (item.Quantity > product.Quantity)
                {
                    _unitOfWork.OrderItems.Remove(item);

                    await _unitOfWork.CommitAsync();

                    _rabbitMQProducer.Publish(new AddNotificationEvent
                    {
                        UserId = item.Order.CustomerId,
                        Title = "Удаление товара из заказа",
                        Message = $"Обратите внимание! Продукт {item.CatalogName} {item.ProductName} был удалён " +
                            $"из заказа из-за отсутствия его в нужном количестве",
                        Type = NotificationType.Warning
                    });
                }
                else
                {
                    item.ProductName = product.ProductName;
                    item.CatalogId = product.CatalogId;
                    item.CatalogName = product.CatalogName;
                    _unitOfWork.OrderItems.Update(item);

                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
