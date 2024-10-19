using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
