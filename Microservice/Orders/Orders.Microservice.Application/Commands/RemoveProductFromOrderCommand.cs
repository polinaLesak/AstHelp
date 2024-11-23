using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class RemoveProductFromOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
