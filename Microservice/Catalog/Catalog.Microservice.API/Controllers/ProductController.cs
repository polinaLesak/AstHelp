using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Product
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "0, 1, 2, 3")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
        }

        // GET: api/Product/Products?ids=&ids=
        [HttpGet("Products")]
        [Authorize(Roles = "0, 1, 2, 3")]
        public async Task<List<Product>> GetProductsInfoById([FromQuery] Guid[] ids)
        {
            return await _mediator.Send(new GetProductsInfoByIdsQuery(ids));
        }

        // POST: api/Product
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/Product
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE: api/Product
        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return Ok();
        }
    }
}
