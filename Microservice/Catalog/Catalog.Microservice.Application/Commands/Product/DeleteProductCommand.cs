using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public Guid ProductId { get; }

        public DeleteProductCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}
