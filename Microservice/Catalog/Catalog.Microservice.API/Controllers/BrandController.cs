using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Brand
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetAllBrands()
        {
            return Ok(await _mediator.Send(new GetAllBrandsQuery()));
        }

        // GET: api/Brand/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            return Ok(await _mediator.Send(new GetBrandByIdQuery(id)));
        }

        // POST: api/Brand
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/Brand
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // DELETE: api/Brand?id=
        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteBrand(
            [FromQuery] int id)
        {
            await _mediator.Send(new DeleteBrandCommand(id));
            return Ok();
        }
    }
}
