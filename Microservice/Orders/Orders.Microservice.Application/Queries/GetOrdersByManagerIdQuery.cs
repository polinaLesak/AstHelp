using MediatR;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.Queries
{
    public class GetOrdersByManagerIdQuery : IRequest<IEnumerable<Order>>
    {
        public int ManagerId { get; }

        public GetOrdersByManagerIdQuery(int managerId)
        {
            ManagerId = managerId;
        }
    }
}
