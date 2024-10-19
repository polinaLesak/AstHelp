using Identity.Microservice.Domain.Entities;
using MediatR;

namespace Identity.Microservice.Application.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
