using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.DTOs;
using Cart.Microservice.Application.Exceptions;
using Cart.Microservice.Application.Handlers;
using Cart.Microservice.Application.Service;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using Moq;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Handlers
{
    public class AddProductToCartCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<CatalogService> _catalogServiceMock;
        private readonly AddProductToCartCommandHandler _handler;

        public AddProductToCartCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _catalogServiceMock = new Mock<CatalogService>(MockBehavior.Loose);
            _handler = new AddProductToCartCommandHandler(_unitOfWorkMock.Object, _catalogServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddNewCartItem_WhenCartExistsAndProductNotInCart()
        {
            var command = new AddProductToCartCommand
            {
                UserId = 1,
                ProductId = Guid.NewGuid(),
                Quantity = 2
            };

            var productInfo = new ProductInfoDto
            {
                CatalogId = 1,
                CatalogName = "Test Catalog",
                ProductId = command.ProductId,
                ProductName = "Test Product"
            };

            var cart = new CartEntity
            {
                UserId = command.UserId,
                Items = new List<CartItem>()
            };

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(command.UserId))
                .ReturnsAsync(cart);
            _catalogServiceMock.Setup(x => x.GetProductInfoAsync(command.ProductId))
                .ReturnsAsync(productInfo);
            _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal(productInfo.ProductId, result.Items.First().ProductId);
            Assert.Equal(command.Quantity, result.Items.First().Quantity);

            _unitOfWorkMock.Verify(x => x.Carts.Update(cart), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenProductNotFoundInCatalog()
        {
            var command = new AddProductToCartCommand
            {
                UserId = 1,
                ProductId = Guid.NewGuid(),
                Quantity = 2
            };

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(command.UserId))
                .ReturnsAsync((CartEntity)null);
            _catalogServiceMock.Setup(x => x.GetProductInfoAsync(command.ProductId))
                .ReturnsAsync((ProductInfoDto)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
