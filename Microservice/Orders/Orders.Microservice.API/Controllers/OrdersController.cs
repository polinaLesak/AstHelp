using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Queries;

namespace Orders.Microservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Orders
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            return Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
        }

        // GET: api/Orders/Customer/{customerId}
        [HttpGet("Customer/{customerId}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> GetOrdersByUserId(int customerId)
        {
            return Ok(await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId)));
        }

        // GET: api/Orders/Manager/{managerId}
        [HttpGet("Manager/{managerId}")]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> GetOrdersByManagerId(int managerId)
        {
            return Ok(await _mediator.Send(new GetOrdersByManagerIdQuery(managerId)));
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/Orders/UpdateStatus/{id}
        [HttpPut("UpdateStatus/{id}")]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, UpdateOrderStatusCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
