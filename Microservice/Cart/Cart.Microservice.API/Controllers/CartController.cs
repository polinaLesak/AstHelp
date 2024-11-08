using Cart.Microservice.Application.Commands;
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

        // GET: api/Cart?userId=
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<Domain.Entities.Cart> GetCart(
            [FromQuery] int userId)
        {
            return await _mediator.Send(new GetCartQuery(userId));
        }

        // GET: api/Cart/ProductsCount?userId=
        [HttpGet("ProductsCount")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<int> GetCartProductsCount(
            [FromQuery] int userId)
        {
            return await _mediator.Send(new GetCartProductsCount(userId));
        }

        // POST: api/Cart/AddProduct
        [HttpPost("AddProduct")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<Domain.Entities.Cart> AddToCart(
            [FromBody] AddProductToCartCommand command)
        {
            return await _mediator.Send(command);
        }

        // POST: api/Cart/Clear?userId=
        [HttpPost("Clear")]
        [Authorize(Roles = "0, 1, 2, 3")]
        public async Task ClearCart(
            [FromQuery] int userId)
        {
            await _mediator.Send(new ClearCartByUserIdCommand(userId));
        }

        // DELETE: api/Cart/{userId}/DeleteProduct?productId=
        [HttpDelete("{userId}/DeleteProduct")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task RemoveFromCart(
            [FromQuery] Guid productId,
            int userId)
        {
            await _mediator.Send(new RemoveProductFromCartCommand(userId, productId));
        }
    }
}
