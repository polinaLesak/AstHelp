using Identity.Microservice.Application.DTOs.Response;
using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class LoginUserCommand : IRequest<UserLoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
