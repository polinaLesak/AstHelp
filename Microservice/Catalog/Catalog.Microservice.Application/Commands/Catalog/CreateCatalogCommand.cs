using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class CreateCatalogCommand : IRequest<Domain.Entities.Catalog>
    {
        public string Name { get; set; } = "";
        public int[] AttributeIds { get; set; } = [];
    }
}
