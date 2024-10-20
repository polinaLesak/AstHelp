using Identity.Microservice.Application.DTOs.Response;
using MediatR;

namespace Identity.Microservice.Application.Commands
{
    public class LoginMachineCommand : IRequest<MachineLoginResponseDto>
    {
        public string ApiKey { get; set; } = "";
    }
}
