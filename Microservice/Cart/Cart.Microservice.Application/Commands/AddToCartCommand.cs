using Cart.Microservice.Application.DTOs.Command;
using MediatR;

namespace Cart.Microservice.Application.Commands
{
    public class AddToCartCommand : IRequest<Domain.Entities.Cart>
    {
        public int UserId { get; set; }
        public AddToCartCommandDto Item { get; set; }

        public AddToCartCommand(int userId, AddToCartCommandDto item)
        {
            UserId = userId;
            Item = item;
        }
    }
}
