using Identity.Microservice.API.Controllers;
using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Queries;
using Identity.Microservice.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Identity.Microservice.Tests.Unit.Controller
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserController _controller;
        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfUsers()
        {
            var users = new List<User> { new User() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default))
                .ReturnsAsync(users);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult_WithUser()
        {
            var user = new User();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default))
                .ReturnsAsync(user);

            var result = await _controller.GetUserById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task DeactivateUser_ReturnsOkResult()
        {
            var result = await _controller.DeactivateUser(1);

            Assert.IsType<OkResult>(result);
            _mediatorMock.Verify(m => m.Send(It.IsAny<DeactivateUserCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOkResult()
        {
            var result = await _controller.DeleteUser(1);

            Assert.IsType<OkResult>(result);
            _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteUserCommand>(), default), Times.Once);
        }
    }
}
