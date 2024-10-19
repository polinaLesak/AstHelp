using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Microservice.Application.Handlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Profile>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            return profile;
        }
    }
}
