using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class UpdateCatalogCommand : IRequest<Domain.Entities.Catalog>
    {
        public int CatalogId { get; set; }
        public string Name { get; set; } = "";
        public int[] AttributeIds { get; set; } = [];
    }
}
