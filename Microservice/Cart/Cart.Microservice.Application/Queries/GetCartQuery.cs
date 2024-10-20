using MediatR;

namespace Cart.Microservice.Application.Queries
{
    public class GetCartQuery : IRequest<Domain.Entities.Cart>
    {
        public int UserId { get; set; }

        public GetCartQuery(int userId)
        {
            UserId = userId;
        }
    }
}
