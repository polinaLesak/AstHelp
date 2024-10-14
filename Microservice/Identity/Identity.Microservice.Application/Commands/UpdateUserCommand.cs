using Identity.Microservice.Domain.Entities;
using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class UpdateUserCommand : IRequest<User>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
