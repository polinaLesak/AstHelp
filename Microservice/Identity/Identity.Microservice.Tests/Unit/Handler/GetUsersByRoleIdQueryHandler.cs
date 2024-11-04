using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Application.Queries;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class GetUsersByRoleIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetUsersByRoleIdQueryHandler _handler;

        public GetUsersByRoleIdQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetUsersByRoleIdQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRoleId_ReturnsUsers()
        {
            var roleId = 1;
            var users = new List<User> { new User { Username = "user1" }, new User { Username = "user2" } };
            _unitOfWorkMock.Setup(u => u.Users.GetUsersByRoleIdAsync(roleId))
                .ReturnsAsync(users);

            var query = new GetUsersByRoleIdQuery(roleId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(users, result);
        }

        [Fact]
        public async Task Handle_NoUsers_ReturnsEmptyList()
        {
            var roleId = 1;
            var users = new List<User>();
            _unitOfWorkMock.Setup(u => u.Users.GetUsersByRoleIdAsync(roleId))
                .ReturnsAsync(users);

            var query = new GetUsersByRoleIdQuery(roleId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Empty(result);
        }
    }
}
