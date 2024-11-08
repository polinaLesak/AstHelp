using MediatR;

namespace Cart.Microservice.Application.Commands
{
    public class AddProductToCartCommand : IRequest<Domain.Entities.Cart>
    {
        public int UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
