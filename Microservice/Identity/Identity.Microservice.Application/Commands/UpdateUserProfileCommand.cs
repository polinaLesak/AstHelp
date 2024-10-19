using Identity.Microservice.Domain.Entities;
using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class UpdateUserProfileCommand : IRequest<Profile>
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = "";
        public string Position { get; set; } = "";
    }
}
