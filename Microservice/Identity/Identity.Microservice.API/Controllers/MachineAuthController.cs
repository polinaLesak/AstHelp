using Identity.Microservice.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MachineAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/MachineAuth/Token
        [HttpPost("Token")]
        public async Task<IActionResult> GetMachineToken([FromBody] LoginMachineCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
