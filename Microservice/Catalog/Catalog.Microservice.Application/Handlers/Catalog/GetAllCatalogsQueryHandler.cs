using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetAllCatalogsQueryHandler : IRequestHandler<GetAllCatalogsQuery, IEnumerable<Domain.Entities.Catalog>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCatalogsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.Catalog>> Handle(GetAllCatalogsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Catalogs.GetAllAsync();
        }
    }
}
