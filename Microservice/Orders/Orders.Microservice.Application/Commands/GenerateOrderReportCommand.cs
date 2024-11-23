using MediatR;

namespace Orders.Microservice.Application.Commands
{
    public class GenerateOrderReportCommand : IRequest<byte[]>
    {
        public Guid OrderId { get; set; }

        public GenerateOrderReportCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
