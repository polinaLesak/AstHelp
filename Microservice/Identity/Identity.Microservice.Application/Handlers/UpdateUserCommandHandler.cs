using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Events;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Messaging;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
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

            _rabbitMQProducer.Publish(new UserUpdatedEvent(user));

            return user;
        }
    }
}
