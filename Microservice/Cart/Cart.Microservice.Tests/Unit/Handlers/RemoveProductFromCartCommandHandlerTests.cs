using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.Exceptions;
using Cart.Microservice.Application.Handlers;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using Moq;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Handlers
{
    public class RemoveProductFromCartCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RemoveProductFromCartCommandHandler _handler;

        public RemoveProductFromCartCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new RemoveProductFromCartCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRemoveCartItem_WhenItemExists()
        {
            var command = new RemoveProductFromCartCommand(1, Guid.NewGuid());

            var cart = new CartEntity
            {
                UserId = command.UserId,
                Items = new List<CartItem>
            {
                new CartItem { Id = command.ItemId }
            }
            };

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(command.UserId))
                .ReturnsAsync(cart);
            _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Empty(cart.Items);

            _unitOfWorkMock.Verify(x => x.Carts.Update(cart), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenCartItemDoesNotExist()
        {
            var command = new RemoveProductFromCartCommand(1, Guid.NewGuid());

            var cart = new CartEntity
            {
                UserId = command.UserId,
                Items = new List<CartItem>()
            };

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(command.UserId))
                .ReturnsAsync(cart);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
