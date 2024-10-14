using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"Пользователь с ID \"{request.UserId}\" не найден.");
            }

            user.IsActive = !user.IsActive;
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
