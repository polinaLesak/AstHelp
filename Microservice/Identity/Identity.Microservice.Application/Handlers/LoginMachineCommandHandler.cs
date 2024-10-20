using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.DTOs.Response;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Application.Services;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class LoginMachineCommandHandler : IRequestHandler<LoginMachineCommand, MachineLoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginMachineCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<MachineLoginResponseDto> Handle(LoginMachineCommand request, CancellationToken cancellationToken)
        {
            var machineAccount = await _unitOfWork.MachineAccounts.GetByApiKeyAsync(request.ApiKey);

            if (machineAccount == null)
            {
                throw new UnauthorizedException("Invalid API Key.");
            }

            var token = _tokenService.GenerateMachineToken(machineAccount.ServiceName);

            return new MachineLoginResponseDto
            {
                Token = _tokenService.GenerateMachineToken(machineAccount.ServiceName)
            };
        }
    }
}
