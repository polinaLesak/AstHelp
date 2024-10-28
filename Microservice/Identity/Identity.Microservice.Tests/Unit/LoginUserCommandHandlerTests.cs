using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Application.Services;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit
{
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly LoginUserCommandHandler _handler;

        public LoginUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _tokenServiceMock = new Mock<ITokenService>();
            _handler = new LoginUserCommandHandler(_unitOfWorkMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
        {
            var command = new LoginUserCommand("testUser", "password");
            var user = new User { Username = "testUser", Password = BCrypt.Net.BCrypt.HashPassword("password"), IsActive = true };
            _unitOfWorkMock.Setup(uow => uow.Users.GetUserByUsernameAsync(command.Username)).ReturnsAsync(user);
            _tokenServiceMock.Setup(ts => ts.GenerateToken(user)).Returns("jwt_token");

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("jwt_token", result.JwtToken);
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenUserNotFound()
        {
            var command = new LoginUserCommand("invalidUser", "password");
            _unitOfWorkMock.Setup(uow => uow.Users.GetUserByUsernameAsync(command.Username)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
