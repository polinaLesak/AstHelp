using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class UpdateAttributeCommandHandler : IRequestHandler<UpdateAttributeCommand, Domain.Entities.Attribute>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAttributeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Attribute> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var attribute = await _unitOfWork.Attributes.GetByIdAsync(request.AttributeId);
            if (attribute == null)
            {
                throw new NotFoundException($"Атрибут с ID \"{request.AttributeId}\" не найден.");
            }

            AttributeType attributeType = await _unitOfWork.AttributeTypes.GetByIdAsync(request.AttributeTypeId);
            if (attributeType == null)
            {
                throw new NotFoundException("Данный тип атрибута не найден.");
            }

            attribute.Name = request.Name;
            attribute.AttributeTypeId = request.AttributeTypeId;

            _unitOfWork.Attributes.Update(attribute);
            await _unitOfWork.CommitAsync();

            return attribute;
        }
    }
}
