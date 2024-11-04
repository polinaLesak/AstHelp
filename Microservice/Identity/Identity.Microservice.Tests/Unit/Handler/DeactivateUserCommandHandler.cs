using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class DeactivateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeactivateUserCommandHandler _handler;

        public DeactivateUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeactivateUserCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsNotFoundException()
        {
            var command = new DeactivateUserCommand { UserId = 1 };
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserFound_ChangesUserStateAndCommits()
        {
            var user = new User { IsActive = true };
            var command = new DeactivateUserCommand { UserId = 1 };
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync(user);

            await _handler.Handle(command, CancellationToken.None);

            Assert.False(user.IsActive);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
