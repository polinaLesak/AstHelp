using MediatR;

namespace Orders.Microservice.Application.Events
{
    public class DeleteProductEvent : INotification
    {
        public Guid ProductId { get; set; }
    }
}
