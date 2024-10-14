using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.DTOs.Response;
using Identity.Microservice.Application.Services;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserLoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(request.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Аккаунт не найден.");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Ваш аккаунт деактивирован. Обратитесь к администратору.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Неверный логин или пароль.");

            return new UserLoginResponseDto()
            {
                Username = user.Username,
                JwtToken = _tokenService.GenerateToken(user)
            };
        }
    }
}
