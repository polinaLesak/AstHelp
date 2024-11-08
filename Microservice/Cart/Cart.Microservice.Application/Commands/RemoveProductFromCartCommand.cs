using MediatR;

namespace Cart.Microservice.Application.Commands
{
    public class RemoveProductFromCartCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public Guid ItemId { get; set; }

        public RemoveProductFromCartCommand(int userId, Guid itemId)
        {
            UserId = userId;
            ItemId = itemId;
        }
    }
}
