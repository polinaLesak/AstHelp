using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class GenerateOrderActCommand : IRequest<byte[]>
    {
        public Guid OrderId { get; set; }

        public GenerateOrderActCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
