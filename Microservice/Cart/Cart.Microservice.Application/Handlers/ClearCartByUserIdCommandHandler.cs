using Cart.Microservice.Application.Commands;
using Cart.Microservice.Domain.Repositories;
using MediatR;

namespace Cart.Microservice.Application.Handlers
{
    public class ClearCartByUserIdCommandHandler : IRequestHandler<ClearCartByUserIdCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClearCartByUserIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ClearCartByUserIdCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(request.UserId);
            if(cart != null)
            {
                cart.Items.Clear();
                _unitOfWork.Carts.Remove(cart);
                await _unitOfWork.CommitAsync();
            }

            return Unit.Value;
        }
    }
}
