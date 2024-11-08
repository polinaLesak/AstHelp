using MediatR;

namespace Cart.Microservice.Application.Queries
{
    public class GetCartProductsCount : IRequest<int>
    {
        public int UserId { get; set; }

        public GetCartProductsCount(int userId)
        {
            UserId = userId;
        }
    }
}
