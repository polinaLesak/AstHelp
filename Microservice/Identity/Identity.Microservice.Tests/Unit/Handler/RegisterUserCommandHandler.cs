using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class RegisterUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new RegisterUserCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_UsernameAlreadyExists_ThrowsUnauthorizedAccessException()
        {
            var command = new RegisterUserCommand { Username = "existingUser" };
            _unitOfWorkMock.Setup(u => u.Users.GetUserByUsernameAsync(command.Username))
                .ReturnsAsync(new User());

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesUser()
        {
            var command = new RegisterUserCommand
            {
                Username = "newUser",
                Email = "new@example.com",
                Password = "password",
                Fullname = "Full Name",
                Position = "Position"
            };

            _unitOfWorkMock.Setup(u => u.Users.GetUserByUsernameAsync(command.Username))
                .ReturnsAsync((User)null);

            var role = new Role { Id = 3 };
            _unitOfWorkMock.Setup(u => u.Roles.GetByIdAsync(3)).ReturnsAsync(role);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.Profiles.AddAsync(It.IsAny<Profile>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
