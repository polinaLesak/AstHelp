using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"Пользователь с ID \"{request.UserId}\" не найден.");
            }

            user.Username = request.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Email = request.Email;

            await _unitOfWork.CommitAsync();

            return user;
        }
    }
}
