using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.Exceptions;
using Cart.Microservice.Application.Service;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using MediatR;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Application.Handlers
{
    public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand, CartEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogService _catalogService;

        public AddProductToCartCommandHandler(IUnitOfWork unitOfWork, CatalogService catalogService)
        {
            _unitOfWork = unitOfWork;
            _catalogService = catalogService;
        }

        public async Task<CartEntity> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(request.UserId);
            if (cart == null)
            {
                cart = new CartEntity
                {
                    UserId = request.UserId
                };

                await _unitOfWork.Carts.AddAsync(cart);
            }

            var productInfo = await _catalogService.GetProductInfoAsync(request.ProductId)
                ?? throw new NotFoundException($"Информация о продукте с ID {request.ProductId} не найдена");

            CartItem? cartItem = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (cartItem == null)
            {
                var item = new CartItem
                {
                    CatalogId = productInfo.CatalogId,
                    CatalogName = productInfo.CatalogName,
                    ProductId = request.ProductId,
                    ProductName = productInfo.ProductName,
                    Quantity = request.Quantity
                };
                cart.Items.Add(item);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
            }

            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.CommitAsync();

            return cart;
        }
    }
}
