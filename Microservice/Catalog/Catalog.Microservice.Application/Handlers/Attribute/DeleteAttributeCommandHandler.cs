using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class DeleteAttributeCommandHandler : IRequestHandler<DeleteAttributeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAttributeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
        {
            var attribute = await _unitOfWork.Attributes.GetByIdAsync(request.AttributeId);
            if (attribute == null)
            {
                throw new NotFoundException($"Атрибут с ID \"{request.AttributeId}\" не найден.");
            }

            try
            {
                _unitOfWork.Attributes.Remove(attribute);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw new DataExistsException($"Данный атрибут с ID \"{request.AttributeId}\" используется. Удаление невозможно.");
            }

            return Unit.Value;
        }
    }
}
