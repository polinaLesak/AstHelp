using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
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

        // POST: api/User/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка.", details = ex.Message });
            }
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка.", details = ex.Message });
            }
        }

        // POST: api/User/Deactivate?id=
        [HttpPost("Deactivate")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeactivateUser([FromQuery] int id)
        {
            try
            {
                var result = await _mediator.Send(new DeactivateUserCommand { UserId = id });
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка.", details = ex.Message });
            }
        }

        // PUT: api/User/Update
        [HttpPut("Update")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка.", details = ex.Message });
            }
        }

        // PUT: api/User/Update/Profile
        [HttpPut("Update/Profile")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка.", details = ex.Message });
            }
        }

        // POST: api/User/Delete?id=
        [HttpDelete("Delete")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteUserCommand { UserId = id });
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка.", details = ex.Message });
            }
        }
    }
}
