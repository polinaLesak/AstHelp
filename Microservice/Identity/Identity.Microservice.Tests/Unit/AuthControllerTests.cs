using Identity.Microservice.API.Controllers;
using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Identity.Microservice.Tests.Unit
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenCommandIsSuccessful()
        {
            var command = new RegisterUserCommand { Username = "testUser", Email = "test@example.com", Password = "password" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            var result = await _controller.Register(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCommandIsSuccessful()
        {
            var command = new LoginUserCommand("testUser", "password");
            var expectedResponse = new UserLoginResponseDto { Username = "testUser", JwtToken = "token" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.Login(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse, okResult.Value);
        }
    }

}
