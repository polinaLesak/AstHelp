using Identity.Microservice.Domain.Entities;
using MediatR;

namespace Identity.Microservice.Application.Queries
{
    public class GetUsersByRoleIdQuery : IRequest<IEnumerable<User>>
    {
        public int RoleId { get; set; }

        public GetUsersByRoleIdQuery(int roleId)
        {
            RoleId = roleId;
        }
    }
}
