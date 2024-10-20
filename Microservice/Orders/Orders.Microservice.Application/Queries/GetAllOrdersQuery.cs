using MediatR;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
    {

    }
}
