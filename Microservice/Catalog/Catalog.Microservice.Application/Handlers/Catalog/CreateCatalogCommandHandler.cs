using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, Domain.Entities.Catalog>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCatalogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Catalog> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Catalogs.ExistCatalogByName(request.Name))
            {
                throw new NotFoundException("Данная категория уже существует.");
            }

            var catalog = new Domain.Entities.Catalog
            {
                Name = request.Name,
                CatalogAttributes = new List<CatalogAttribute>()
            };

            foreach (var attributeId in request.AttributeIds)
            {
                var attribute = await _unitOfWork.Attributes.ExistAttributeById(attributeId);
                if (!(await _unitOfWork.Attributes.ExistAttributeById(attributeId)))
                {
                    throw new NotFoundException($"Атрибут с ID {attributeId} не найден.");
                }

                catalog.CatalogAttributes.Add(new CatalogAttribute
                {
                    Catalog = catalog,
                    AttributeId = attributeId
                });
            }


            await _unitOfWork.Catalogs.AddAsync(catalog);
            await _unitOfWork.CommitAsync();

            return catalog;
        }
    }
}
