using Cart.Microservice.Application.Commands;
using Cart.Microservice.Application.DTOs.Command;
using Cart.Microservice.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Microservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Cart/{userId}
        [HttpGet("{userId}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetCart(int userId)
        {
            return Ok(await _mediator.Send(new GetCartQuery(userId)));
        }

        // POST: api/Cart/{userId}/Add
        [HttpPost("{userId}/Add")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] AddToCartCommandDto dto)
        {
            return Ok(await _mediator.Send(new AddToCartCommand(userId, dto)));
        }

        // DELETE: api/Cart/{userId}/Delete
        [HttpDelete("{userId}/Delete/{productId}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> RemoveFromCart(int userId, Guid productId)
        {
            return Ok(await _mediator.Send(new RemoveFromCartCommand(userId, productId)));
        }
    }
}
