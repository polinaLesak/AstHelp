using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;

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
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _mediator.Send(new GetAllOrdersQuery());
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<Order> GetOrderById(Guid id)
        {
            return await _mediator.Send(new GetOrderByIdQuery(id));
        }

        // GET: api/Orders/Customer/{customerId}
        [HttpGet("Customer/{customerId}")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int customerId)
        {
            return await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId));
        }

        // GET: api/Orders/Manager/{managerId}
        [HttpGet("Manager/{managerId}")]
        [Authorize(Roles = "1, 2")]
        public async Task<IEnumerable<Order>> GetOrdersByManagerId(int managerId)
        {
            return await _mediator.Send(new GetOrdersByManagerIdQuery(managerId));
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<Order> CreateOrder(CreateOrderCommand command)
        {
            return await _mediator.Send(command);
        }

        // PUT: api/Orders/UpdateStatus?orderId=&status=
        [HttpPut("UpdateStatus")]
        [Authorize(Roles = "1, 2")]
        public async Task UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            await _mediator.Send(new UpdateOrderStatusCommand
            {
                OrderId = orderId,
                NewStatus = status
            });
        }

        // PUT: api/Orders/RemoveProduct?orderId=&productId=
        [HttpPut("RemoveProduct")]
        [Authorize(Roles = "1, 2")]
        public async Task UpdateOrderStatus(Guid orderId, Guid productId)
        {
            await _mediator.Send(new RemoveProductFromOrderCommand
            {
                OrderId = orderId,
                ProductId = productId
            });
        }

        // Get: api/Orders/GenerateAct?orderId=
        [HttpGet("GenerateAct")]
        [Authorize(Roles = "1")]
        public async Task<FileResult> GenerateOrderAct(Guid orderId)
        {
            var fileBytes = await _mediator.Send(new GenerateOrderActCommand(orderId));
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Act_{orderId}.docx");
        }

        // Get: api/Orders/GenerateReport?orderId=
        [HttpGet("GenerateReport")]
        [Authorize(Roles = "2")]
        public async Task<FileResult> GenerateReport(Guid orderId)
        {
            var fileBytes = await _mediator.Send(new GenerateOrderReportCommand(orderId));
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Report_{orderId}.docx");
        }
    }
}
