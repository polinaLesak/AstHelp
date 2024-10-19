using Identity.Microservice.Application.Commands;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Users.GetUserByUsernameAsync(request.Username) != null)
            {
                throw new UnauthorizedAccessException("Данный логин уже существует.");
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
            };


            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.IsActive = false;
            user.UserRoles.Add(new UserRole
            {
                User = user,
                Role = await _unitOfWork.Roles.GetByIdAsync(3)
            });
            user.Profile = new Profile
            {
                Fullname = request.Fullname,
                Position = request.Position,
                User = user
            };

            await _unitOfWork.Profiles.AddAsync(user.Profile);
            await _unitOfWork.CommitAsync();

            return user != null;
        }
    }
}
