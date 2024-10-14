using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
    }
}
