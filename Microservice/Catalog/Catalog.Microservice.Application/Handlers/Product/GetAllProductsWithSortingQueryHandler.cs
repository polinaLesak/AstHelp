using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetAllProductsWithSortingQueryHandler : IRequestHandler<GetAllProductsWithSortingQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsWithSortingQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsWithSortingQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Products.GetAllAsyncWithSorting(request.Sorting);
        }
    }
}
