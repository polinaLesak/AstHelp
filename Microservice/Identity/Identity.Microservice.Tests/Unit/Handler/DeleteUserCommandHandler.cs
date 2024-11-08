using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteUserCommandHandler _handler;

        public DeleteUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            //_handler = new DeleteUserCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsNotFoundException()
        {
            var command = new DeleteUserCommand { UserId = 1 };
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserFound_DeletesUserAndCommits()
        {
            var user = new User();
            var command = new DeleteUserCommand { UserId = 1 };
            _unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync(user);

            await _handler.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(u => u.Users.Remove(user), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
