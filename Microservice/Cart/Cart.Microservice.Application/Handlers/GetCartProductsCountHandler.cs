using Cart.Microservice.Application.Queries;
using Cart.Microservice.Domain.Repositories;
using MediatR;

namespace Cart.Microservice.Application.Handlers
{
    public class GetCartProductsCountHandler : IRequestHandler<GetCartProductsCount, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartProductsCountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetCartProductsCount request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CartItems.GetCartProductsCountByUserId(request.UserId);
        }
    }
}
