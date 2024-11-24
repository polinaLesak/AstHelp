using Cart.Microservice.API.Controllers;
using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.Queries;
using Cart.Microservice.Domain.Entities;
using MediatR;
using Moq;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Notification.Microservice.Tests.Unit.Controller
{
    public class CartControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CartController _controller;

        public CartControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CartController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCart_ShouldReturnCart()
        {
            var userId = 1;
            var cart = new CartEntity
            {
                UserId = userId,
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 }
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCartQuery>(), default))
                .ReturnsAsync(cart);

            var result = await _controller.GetCart(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Single(result.Items);

            _mediatorMock.Verify(m => m.Send(It.Is<GetCartQuery>(q => q.UserId == userId), default), Times.Once);
        }

        [Fact]
        public async Task GetCartProductsCount_ShouldReturnProductsCount()
        {
            var userId = 1;
            var productCount = 5;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCartProductsCount>(), default))
                .ReturnsAsync(productCount);

            var result = await _controller.GetCartProductsCount(userId);

            Assert.Equal(productCount, result);

            _mediatorMock.Verify(m => m.Send(It.Is<GetCartProductsCount>(q => q.UserId == userId), default), Times.Once);
        }

        [Fact]
        public async Task AddToCart_ShouldReturnUpdatedCart()
        {
            var command = new AddProductToCartCommand
            {
                UserId = 1,
                ProductId = Guid.NewGuid(),
                Quantity = 3
            };

            var updatedCart = new CartEntity
            {
                UserId = command.UserId,
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = command.ProductId, Quantity = command.Quantity }
                }
            };

            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(updatedCart);

            var result = await _controller.AddToCart(command);

            Assert.NotNull(result);
            Assert.Equal(command.UserId, result.UserId);
            Assert.Single(result.Items);

            _mediatorMock.Verify(m => m.Send(command, default), Times.Once);
        }

        [Fact]
        public async Task ClearCart_ShouldCallMediator()
        {
            var userId = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<ClearCartByUserIdCommand>(), default))
                .ReturnsAsync(MediatR.Unit.Value);

            await _controller.ClearCart(userId);

            _mediatorMock.Verify(m => m.Send(It.Is<ClearCartByUserIdCommand>(c => c.UserId == userId), default), Times.Once);
        }

        [Fact]
        public async Task RemoveFromCart_ShouldCallMediator()
        {
            var userId = 1;
            var productId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<RemoveProductFromCartCommand>(), default))
                .ReturnsAsync(MediatR.Unit.Value);

            await _controller.RemoveFromCart(productId, userId);

            _mediatorMock.Verify(m => m.Send(It.Is<RemoveProductFromCartCommand>(c => c.UserId == userId && c.ItemId == productId), default), Times.Once);
        }
    }
}
