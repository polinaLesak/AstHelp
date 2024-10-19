using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAttributesByCatalogIdQuery : IRequest<IEnumerable<Domain.Entities.Attribute>>
    {
        public int CatalogId { get; set; }

        public GetAttributesByCatalogIdQuery(int catalogId)
        {
            CatalogId = catalogId;
        }
    }
}
