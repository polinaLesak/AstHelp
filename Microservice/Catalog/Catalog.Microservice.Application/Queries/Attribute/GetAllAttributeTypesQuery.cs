using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAllAttributeTypesQuery : IRequest<IEnumerable<Domain.Entities.AttributeType>>
    {
    }
}
