using MediatR;
using Orders.Microservice.Application.Events;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Messaging;
using Orders.Microservice.Infrastructure.Messaging.Events;

namespace Orders.Microservice.Application.EventsHandlers
{
    public class DeleteProductEventHandler : INotificationHandler<DeleteProductEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public DeleteProductEventHandler(IUnitOfWork unitOfWork, RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task Handle(DeleteProductEvent product, CancellationToken cancellationToken)
        {
            var cartItems = await _unitOfWork.OrderItems.GetAllOrderItemsByProductId(product.ProductId);
            foreach (var item in cartItems)
            {
                _unitOfWork.OrderItems.Remove(item);
                await _unitOfWork.CommitAsync();
                _rabbitMQProducer.Publish(new AddNotificationEvent
                {
                    UserId = item.Order.CustomerId,
                    Title = "Удаление товара из корзины",
                    Message = $"Обратите внимание! Продукт {item.CatalogName} {item.ProductName} был удалён из системы.",
                    Type = NotificationType.Warning
                });
            }
        }
    }
}
