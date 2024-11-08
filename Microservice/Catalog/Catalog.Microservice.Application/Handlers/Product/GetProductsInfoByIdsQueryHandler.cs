using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetProductsInfoByIdsQueryHandler : IRequestHandler<GetProductsInfoByIdsQuery, List<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsInfoByIdsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> Handle(GetProductsInfoByIdsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Products.GetByIdsAsync(request.Ids);
        }
    }
}
