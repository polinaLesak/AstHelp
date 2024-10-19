using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetCatalogByIdQuery : IRequest<Domain.Entities.Catalog>
    {
        public int Id { get; set; }

        public GetCatalogByIdQuery(int id)
        {
            Id = id;
        }
    }
}
