using Cart.Microservice.Application.Queries;
using Cart.Microservice.Domain.Repositories;
using MediatR;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Application.Handlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CartEntity> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Carts.GetCartByUserIdAsync(request.UserId);
        }
    }
}
