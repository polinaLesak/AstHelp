using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttributeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Attribute
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetAllAttributes()
        {
            return Ok(await _mediator.Send(new GetAllAttributesQuery()));
        }

        // GET: api/Attribute/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetAttributeById(int id)
        {
            return Ok(await _mediator.Send(new GetAttributeByIdQuery(id)));
        }

        // GET: api/Attribute/ByCatalogId?id=
        [HttpGet("ByCatalogId")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetAttributeByCatalogId([FromQuery] int id)
        {
            return Ok(await _mediator.Send(new GetAttributesByCatalogIdQuery(id)));
        }

        // POST: api/Attribute
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateAttribute([FromBody] CreateAttributeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/Attribute
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateAttribute([FromBody] UpdateAttributeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE: api/Attribute?id=
        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteAttribute([FromQuery] int id)
        {
            await _mediator.Send(new DeleteAttributeCommand(id));
            return Ok();
        }
    }
}
