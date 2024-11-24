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
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public UpdateOrderStatusCommandHandler(
            IUnitOfWork unitOfWork,
            RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Заявка с ID \"{request.OrderId}\" не найдена.");

            order.Status = request.NewStatus;

            _unitOfWork.Orders.Update(order);

            if (order.Status == OrderStatus.Packaged)
            {
                _rabbitMQProducer.Publish(new AddNotificationEvent
                {
                    UserId = order.CustomerId,
                    Title = "Готовность заказа",
                    Message = $"Ваш заказ от {order.CreatedAt} готов к выдаче.",
                    Type = NotificationType.Success
                });
            }
            else if (order.Status == OrderStatus.Canceled)
            {
                _rabbitMQProducer.Publish(new AddNotificationEvent
                {
                    UserId = order.CustomerId,
                    Title = "Отмена заказа",
                    Message = $"Обратите внимание! Ваш заказ от {order.CreatedAt} отклонён.",
                    Type = NotificationType.Error
                });
            }
            else
            {
                _rabbitMQProducer.Publish(new AddNotificationEvent
                {
                    UserId = order.CustomerId,
                    Title = "Статус заказа изменён",
                    Message = $"Обратите внимание! Статус заказа от {order.CreatedAt} был изменён.",
                    Type = NotificationType.Info
                });
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
