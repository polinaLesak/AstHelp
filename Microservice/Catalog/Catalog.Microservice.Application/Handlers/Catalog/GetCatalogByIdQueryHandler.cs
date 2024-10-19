using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetCatalogByIdQueryHandler : IRequestHandler<GetCatalogByIdQuery, Domain.Entities.Catalog>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCatalogByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Catalog> Handle(GetCatalogByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Catalogs.GetByIdAsync(request.Id);
        }
    }
}
