using Identity.Microservice.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
