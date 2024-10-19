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

            foreach (var item in request.AttributeTypes)
            {
                if (!catalog.CatalogAttributes.Any(x => x.AttributeId == item))
                {
                    AttributeType attributeType = await _unitOfWork.AttributeTypes.GetByIdAsync(item);
                    if (attributeType == null)
                        throw new NotFoundException($"Атрибут с ID \"{item}\" не найден.");

                    catalog.CatalogAttributes.Add(new Domain.Entities.CatalogAttribute
                    {
                        CatalogId = catalog.Id,
                        AttributeId = attributeType.Id,
                    });
                }
            }

            foreach (var item in catalog.CatalogAttributes)
            {
                if (!request.AttributeTypes.Any(x => x == item.AttributeId))
                {
                    CatalogAttribute catalogAttribute = catalog.CatalogAttributes
                        .Where(x => x.AttributeId == item.AttributeId)
                        .FirstOrDefault();
                    if (catalogAttribute != null)
                        catalog.CatalogAttributes.Remove(catalogAttribute);
                    List<Product> products = await _unitOfWork.Products.GetAllProductsByCatalogIdAsync(catalog.Id);
                    products.ForEach(x =>
                    {
                        AttributeValue attributeValue = x.AttributeValues
                            .Where(x => x.AttributeId == item.AttributeId).FirstOrDefault();
                        if (attributeValue != null)
                            x.AttributeValues.Remove(attributeValue);
                        _unitOfWork.Products.Update(x);
                    });
                }
            }

            catalog.Name = request.Name;

            _unitOfWork.Catalogs.Update(catalog);
            await _unitOfWork.CommitAsync();

            return catalog;
        }
    }
}
