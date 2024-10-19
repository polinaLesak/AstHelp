using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAllAttributesQuery : IRequest<IEnumerable<Domain.Entities.Attribute>>
    {
    }
}
