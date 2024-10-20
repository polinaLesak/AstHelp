using MediatR;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.Commands
{
    public class UpdateOrderStatusCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
