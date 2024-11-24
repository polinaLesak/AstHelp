using Cart.Microservice.Application.Handlers;
using Cart.Microservice.Application.Queries;
using Cart.Microservice.Domain.Entities;
using Cart.Microservice.Domain.Repositories;
using Moq;
using CartEntity = Cart.Microservice.Domain.Entities.Cart;

namespace Cart.Microservice.Tests.Unit.Handlers
{
    public class GetCartQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetCartQueryHandler _handler;

        public GetCartQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetCartQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCart_WhenCartExists()
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

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(userId))
                .ReturnsAsync(cart);

            var query = new GetCartQuery(userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Single(result.Items);

            _unitOfWorkMock.Verify(x => x.Carts.GetCartByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCartDoesNotExist()
        {
            var userId = 1;

            _unitOfWorkMock.Setup(x => x.Carts.GetCartByUserIdAsync(userId))
                .ReturnsAsync((CartEntity)null);

            var query = new GetCartQuery(userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Null(result);

            _unitOfWorkMock.Verify(x => x.Carts.GetCartByUserIdAsync(userId), Times.Once);
        }
    }
}
