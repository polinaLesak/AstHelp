using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class UpdateCatalogCommand : IRequest<Domain.Entities.Catalog>
    {
        public int CatalogId { get; }
        public string Name { get; } = "";
        public int[] AttributeTypes { get; } = [];
    }
}
