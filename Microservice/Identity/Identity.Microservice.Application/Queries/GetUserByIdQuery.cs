using Identity.Microservice.Domain.Entities;
using MediatR;

namespace Identity.Microservice.Application.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
