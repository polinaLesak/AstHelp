using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Application.Queries;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class GetAllUsersQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetAllUsersQueryHandler _handler;

        public GetAllUsersQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetAllUsersQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User { Username = "user1" },
                new User { Username = "user2" }
            };
            _unitOfWorkMock.Setup(u => u.Users.GetAllAsync())
                .ReturnsAsync(users);

            var query = new GetAllUsersQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(users.Count, result.Count);
            Assert.Equal(users, result);
        }

        [Fact]
        public async Task Handle_NoUsers_ReturnsEmptyList()
        {
            var users = new List<User>();
            _unitOfWorkMock.Setup(u => u.Users.GetAllAsync())
                .ReturnsAsync(users);

            var query = new GetAllUsersQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Empty(result);
        }
    }
}
