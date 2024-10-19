using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string Position { get; set; } = "";
    }
}
