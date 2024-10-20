using MediatR;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.Queries
{
    public class GetOrdersByCustomerIdQuery : IRequest<IEnumerable<Order>>
    {
        public int CustomerId { get; }

        public GetOrdersByCustomerIdQuery(int userId)
        {
            CustomerId = userId;
        }
    }
}
