using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAllBrandsQuery : IRequest<IEnumerable<Brand>>
    {
    }
}
