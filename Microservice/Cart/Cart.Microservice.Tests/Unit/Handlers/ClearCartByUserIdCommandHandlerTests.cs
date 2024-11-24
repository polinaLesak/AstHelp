using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.Handlers;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using Moq;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Handlers
{
    public class ClearCartByUserIdCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ClearCartByUserIdCommandHandler _handler;

        public ClearCartByUserIdCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new ClearCartByUserIdCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldClearCart_WhenCartExists()
        {
            var command = new ClearCartByUserIdCommand(1);
            var cart = new CartEntity { UserId = command.UserId, Items = new List<CartItem> { new CartItem() } };

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(command.UserId))
                .ReturnsAsync(cart);
            _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(x => x.Carts.Remove(cart), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldDoNothing_WhenCartDoesNotExist()
        {
            var command = new ClearCartByUserIdCommand(1);

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(command.UserId))
                .ReturnsAsync((CartEntity)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(x => x.Carts.Remove(It.IsAny<CartEntity>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }
    }
}
