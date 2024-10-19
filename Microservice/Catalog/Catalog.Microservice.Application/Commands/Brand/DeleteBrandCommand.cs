using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class DeleteBrandCommand : IRequest<Unit>
    {
        public int BrandId { get; }

        public DeleteBrandCommand(int brandId)
        {
            BrandId = brandId;
        }
    }
}
