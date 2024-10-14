﻿using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            return Unit.Value;
        }
    }
}
