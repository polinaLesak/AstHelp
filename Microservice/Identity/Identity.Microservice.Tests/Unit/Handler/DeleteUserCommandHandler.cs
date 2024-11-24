using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Events;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Messaging;
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
            var rabbitMQProducerMock = new Mock<RabbitMQProducer>();
            _handler = new DeleteUserCommandHandler(_unitOfWorkMock.Object, rabbitMQProducerMock.Object);
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
            var user = new User { Id = 1, Username = "TestUser", Email = "test@example.com" };
            var command = new DeleteUserCommand { UserId = 1 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(command.UserId))
                .ReturnsAsync(user);
            unitOfWorkMock.Setup(u => u.Users.Remove(It.IsAny<User>()));
            unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            var rabbitMQProducerMock = new Mock<RabbitMQProducer>();

            var handler = new DeleteUserCommandHandler(unitOfWorkMock.Object, rabbitMQProducerMock.Object);

            await handler.Handle(command, CancellationToken.None);

            unitOfWorkMock.Verify(u => u.Users.Remove(user), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            rabbitMQProducerMock.Verify(r => r.Publish(It.IsAny<UserDeletedEvent>()), Times.Once);
        }
    }
}
