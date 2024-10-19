using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class CreateAttributeCommandHandler : IRequestHandler<CreateAttributeCommand, Domain.Entities.Attribute>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAttributeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Attribute> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Attributes.ExistAttributeByName(request.Name))
            {
                throw new DataExistsException("Данный атрибут уже существует.");
            }

            AttributeType attributeType = await _unitOfWork.AttributeTypes.GetByIdAsync(request.AttributeTypeId);
            if (attributeType == null)
            {
                throw new NotFoundException("Данный тип атрибута не найден.");
            }

            var attribute = new Domain.Entities.Attribute
            {
                Name = request.Name,
                AttributeTypeId = attributeType.Id,
            };

            await _unitOfWork.Attributes.AddAsync(attribute);
            await _unitOfWork.CommitAsync();

            return attribute;
        }
    }
}
