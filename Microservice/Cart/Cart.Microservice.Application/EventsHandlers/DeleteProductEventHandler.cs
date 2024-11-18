using Cart.Microservice.Application.Events;
using Cart.Microservice.Domain.Repositories;
using Cart.Microservice.Infrastructure.Messaging;
using Cart.Microservice.Infrastructure.Messaging.Events;
using MediatR;

namespace Cart.Microservice.Application.EventsHandlers
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
            var cartItems = await _unitOfWork.CartItems.GetAllCartItemsByProductId(product.ProductId);
            foreach (var item in cartItems)
            {
                _unitOfWork.CartItems.Remove(item);
                await _unitOfWork.CommitAsync();
                _rabbitMQProducer.Publish(new AddNotificationEvent
                {
                    UserId = item.Cart.UserId,
                    Title = "Удаление товара из корзины",
                    Message = $"Обратите внимание! Продукт {item.CatalogName} {item.ProductName} был удалён из системы.",
                    Type = NotificationType.Warning
                });
            }
        }
    }
}
