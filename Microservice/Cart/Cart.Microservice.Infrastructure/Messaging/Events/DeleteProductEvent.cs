using MediatR;

namespace Cart.Microservice.Application.Events
{
    public class DeleteProductEvent : INotification
    {
        public Guid ProductId { get; set; }
    }
}
