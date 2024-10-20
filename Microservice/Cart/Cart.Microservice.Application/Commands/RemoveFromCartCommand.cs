using MediatR;

namespace Cart.Microservice.Application.Commands
{
    public class RemoveFromCartCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public Guid ProductId { get; set; }

        public RemoveFromCartCommand(int userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
