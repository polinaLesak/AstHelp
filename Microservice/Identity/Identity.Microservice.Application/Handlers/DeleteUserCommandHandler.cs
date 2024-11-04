using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Events;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Messaging;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"Пользователь с ID \"{request.UserId}\" не найден.");
            }

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();

            _rabbitMQProducer.Publish(new UserDeletedEvent(user.Id, user.Username, user.Email));

            return Unit.Value;
        }
    }
}
