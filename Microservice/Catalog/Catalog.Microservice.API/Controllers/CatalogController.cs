using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Catalog
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetAllCatalogs()
        {
            return Ok(await _mediator.Send(new GetAllCatalogsQuery()));
        }

        // GET: api/Catalog{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetCatalogById(int id)
        {
            return Ok(await _mediator.Send(new GetCatalogByIdQuery(id)));
        }

        // POST: api/Catalog
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateCatalog([FromBody] CreateCatalogCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/Product
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateCatalog([FromBody] UpdateCatalogCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE: api/Catalog?id=
        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteCatalog([FromQuery] int id)
        {
            await _mediator.Send(new DeleteCatalogCommand(id));
            return Ok();
        }
    }
}
