using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Events;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Messaging;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Profile>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork, RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<Profile> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.Profiles.GetByUserIdAsync(request.UserId);
            if (profile == null)
            {
                throw new NotFoundException($"Профиль пользователя с ID \"{request.UserId}\" не найден.");
            }

            profile.Fullname = request.Fullname;
            profile.Position = request.Position;

            await _unitOfWork.CommitAsync();

            _rabbitMQProducer.Publish(
                new UserUpdatedEvent(
                    await _unitOfWork.Users.GetByIdAsync(request.UserId)
                    )
                );

            return profile;
        }
    }
}
