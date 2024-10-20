using MediatR;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid OrderId { get; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
