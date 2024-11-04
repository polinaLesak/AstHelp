using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Application.Queries;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetUserByIdQueryHandler _handler;

        public GetUserByIdQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetUserByIdQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_UserExists_ReturnsUser()
        {
            var userId = 1;
            var user = new User { Id = userId, Username = "user1" };
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(userId))
                .ReturnsAsync(user);

            var query = new GetUserByIdQuery { Id = userId };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_ReturnsNull()
        {
            var userId = 1;
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(userId))
                .ReturnsAsync((User)null);

            var query = new GetUserByIdQuery { Id = userId };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
