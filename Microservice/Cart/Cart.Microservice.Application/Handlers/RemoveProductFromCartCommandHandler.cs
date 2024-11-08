using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.Exceptions;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using MediatR;

namespace Cart.Microservice.Application.Handlers
{
    public class RemoveProductFromCartCommandHandler : IRequestHandler<RemoveProductFromCartCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductFromCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(request.UserId);
            if (cart == null)
            {
                throw new NotFoundException($"Корзина пользователя с ID \"{request.UserId}\" не найдена.");
            }

            CartItem? cartItem = cart.Items.Where(x => x.Id == request.ItemId).FirstOrDefault();
            if (cartItem == null)
            {
                throw new NotFoundException($"Элемент корзины с ID \"{request.ItemId}\" не найден.");
            }

            cart.Items.Remove(cartItem);

            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
