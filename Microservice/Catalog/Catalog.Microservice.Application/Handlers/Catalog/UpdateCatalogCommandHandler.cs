using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, Domain.Entities.Catalog>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCatalogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Catalog> Handle(UpdateCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await _unitOfWork.Catalogs.GetByIdAsync(request.CatalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Категория с ID \"{request.CatalogId}\" не найдена.");
            }

            if (catalog.Name != request.Name && await _unitOfWork.Catalogs.ExistCatalogByName(request.Name))
            {
                throw new DataExistsException("Данная категория уже существует.");
            }

            catalog.Name = request.Name;
            catalog.CatalogAttributes.Clear();

            foreach (var attributeId in request.AttributeIds)
            {
                if (!(await _unitOfWork.Attributes.ExistAttributeById(attributeId)))
                {
                    throw new NotFoundException($"Атрибут с ID {attributeId} не найден.");
                }

                catalog.CatalogAttributes.Add(new CatalogAttribute
                {
                    CatalogId = catalog.Id,
                    AttributeId = attributeId
                });
            }


            _unitOfWork.Catalogs.Update(catalog);
            await _unitOfWork.CommitAsync();

            return catalog;
        }
    }
}
