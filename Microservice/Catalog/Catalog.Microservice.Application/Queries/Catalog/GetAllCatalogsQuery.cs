using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAllCatalogsQuery : IRequest<IEnumerable<Domain.Entities.Catalog>>
    {
    }
}
