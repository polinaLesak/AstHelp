using Cart.Microservice.Application.Commands;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using MediatR;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Application.Handlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, CartEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CartEntity> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(request.UserId);
            if (cart == null)
            {
                cart = new CartEntity
                {
                    UserId = request.UserId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Carts.AddAsync(cart);
            }

            var item = new CartItem
            {
                ProductId = request.Item.ProductId,
                Quantity = request.Item.Quantity
            };

            cart.Items.Add(item);
            cart.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();

            return cart;
        }
    }
}
