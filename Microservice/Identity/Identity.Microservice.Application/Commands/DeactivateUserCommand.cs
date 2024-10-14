using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class DeactivateUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
    }
}
