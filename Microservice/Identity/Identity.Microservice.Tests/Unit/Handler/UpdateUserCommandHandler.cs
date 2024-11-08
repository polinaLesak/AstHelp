using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            //_handler = new UpdateUserCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsNotFoundException()
        {
            var command = new UpdateUserCommand { UserId = 1 };
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserFound_UpdatesUserAndCommits()
        {
            var existingUser = new User { Username = "oldUsername", Password = "oldPassword", Email = "old@example.com" };
            var command = new UpdateUserCommand
            {
                UserId = 1,
                Username = "newUsername",
                Password = "newPassword",
                Email = "new@example.com"
            };

            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync(existingUser);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal("newUsername", existingUser.Username);
            Assert.NotEqual("oldPassword", existingUser.Password);
            Assert.Equal("new@example.com", existingUser.Email);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
