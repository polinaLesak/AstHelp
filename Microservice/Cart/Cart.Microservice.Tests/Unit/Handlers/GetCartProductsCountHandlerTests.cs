using Cart.Microservice.Application.Handlers;
using Cart.Microservice.Application.Queries;
using Cart.Microservice.Domain.Repositories;
using Moq;

namespace Cart.Microservice.Tests.Unit.Handlers
{
    public class GetCartProductsCountHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetCartProductsCountHandler _handler;

        public GetCartProductsCountHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetCartProductsCountHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProductsCount_WhenCartExists()
        {
            var userId = 1;
            var productCount = 5;

            _unitOfWorkMock.Setup(x => x.CartItems.GetCartProductsCountByUserId(userId))
                .ReturnsAsync(productCount);

            var query = new GetCartProductsCount(userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(productCount, result);

            _unitOfWorkMock.Verify(x => x.CartItems.GetCartProductsCountByUserId(userId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenCartDoesNotExist()
        {
            var userId = 1;

            _unitOfWorkMock.Setup(x => x.CartItems.GetCartProductsCountByUserId(userId))
                .ReturnsAsync(0);

            var query = new GetCartProductsCount(userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result);

            _unitOfWorkMock.Verify(x => x.CartItems.GetCartProductsCountByUserId(userId), Times.Once);
        }
    }
}
