using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get: api/User/AllUsers
        [HttpGet("AllUsers")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        // Get: api/User
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetUserById(
            [FromQuery] int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        // POST: api/User/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // POST: api/User/Deactivate?id=
        [HttpPost("Deactivate")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeactivateUser([FromQuery] int id)
        {
            await _mediator.Send(new DeactivateUserCommand { UserId = id });
            return Ok();
        }

        // PUT: api/User/Update
        [HttpPut("Update")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/User/Update/Profile
        [HttpPut("Update/Profile")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // POST: api/User/Delete?id=
        [HttpDelete("Delete")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            await _mediator.Send(new DeleteUserCommand { UserId = id });
            return Ok();
        }
    }
}
