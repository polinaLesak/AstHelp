using MediatR;

namespace Cart.Microservice.Application.Commands
{
    public class ClearCartByUserIdCommand : IRequest<Unit>
    {
        public int UserId { get; set; }

        public ClearCartByUserIdCommand(int userId)
        {
            UserId = userId;
        }
    }
}
