using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.Exceptions;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using MediatR;

namespace Cart.Microservice.Application.Handlers
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveFromCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(request.UserId);
            if (cart == null)
            {
                throw new NotFoundException($"Корзина пользователя с ID \"{request.UserId}\" не найдена.");
            }

            CartItem cartItem = cart.Items.Where(x => x.ProductId == request.ProductId).FirstOrDefault();
            if (cartItem == null)
            {
                throw new NotFoundException($"Продукт с ID \"{request.ProductId}\" не найден.");
            }

            cart.Items.Remove(cartItem);
            cart.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
