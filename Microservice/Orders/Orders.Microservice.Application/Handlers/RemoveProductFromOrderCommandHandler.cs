using MediatR;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Events;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Messaging;
using Orders.Microservice.Infrastructure.Messaging.Events;

namespace Orders.Microservice.Application.Handlers
{
    public class RemoveProductFromOrderCommandHandler : IRequestHandler<RemoveProductFromOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public RemoveProductFromOrderCommandHandler(
            IUnitOfWork unitOfWork, 
            RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task Handle(RemoveProductFromOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId)
                ?? throw new NotFoundException($"Заявка с ID \"{request.OrderId}\" не найдена.");

            OrderItem? orderItem = order.Items.Where(x => x.ProductId == request.ProductId).FirstOrDefault() 
                ?? throw new NotFoundException($"Продукт заказа с ID \"{request.ProductId}\" не найден.");
            var notification = new AddNotificationEvent
            {
                UserId = order.CustomerId,
                Title = "Изменение вашего заказа",
                Message = $"Менеджер удалил продукт {orderItem.ProductName} из заказа.",
                Type = NotificationType.Warning
            };
            order.Items.Remove(orderItem);

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitAsync();

            _rabbitMQProducer.Publish(notification);
        }
    }
}
