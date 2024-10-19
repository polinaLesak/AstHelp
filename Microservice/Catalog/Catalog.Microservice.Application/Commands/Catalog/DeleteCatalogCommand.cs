using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class DeleteCatalogCommand : IRequest<Unit>
    {
        public int CatalogId { get; }

        public DeleteCatalogCommand(int catalogId)
        {
            CatalogId = catalogId;
        }
    }
}
