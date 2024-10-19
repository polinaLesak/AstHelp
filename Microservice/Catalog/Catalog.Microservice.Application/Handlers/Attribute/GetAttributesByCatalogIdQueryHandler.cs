using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetAttributesByCatalogIdQueryHandler
        : IRequestHandler<GetAttributesByCatalogIdQuery, IEnumerable<Domain.Entities.Attribute>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAttributesByCatalogIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.Attribute>> Handle(
            GetAttributesByCatalogIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.Attributes.GetAllAttributesByCatalogId(request.CatalogId);
        }
    }
}
