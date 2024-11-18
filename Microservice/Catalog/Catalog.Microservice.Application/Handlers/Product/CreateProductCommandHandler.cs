using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Application.Service;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Products.ExistProductByBrandId_CatalogId_Name(
                request.BrandId, request.CatalogId, request.Name))
            {
                throw new DataExistsException("Данный продукт уже существует.");
            }

            if (await _unitOfWork.Brands.GetByIdAsync(request.BrandId) == null)
            {
                throw new NotFoundException($"Бренд с ID \"{request.BrandId}\" не найден.");
            }

            if (await _unitOfWork.Catalogs.GetByIdAsync(request.CatalogId) == null)
            {
                throw new NotFoundException($"Категория с ID \"{request.CatalogId}\" не найдена.");
            }

            var product = new Product
            {
                Name = request.Name,
                Quantity = request.Quantity,
                BrandId = request.BrandId,
                CatalogId = request.CatalogId,
                AttributeValues = new List<AttributeValue>()
            };

            if (request.Image != null)
            {
                product.ImageUrl = await _fileService.UploadFileAsync(request.Image, "images");
            }

            foreach (var item in request.ProductAttributes)
            {
                var attribute = await _unitOfWork.Attributes.GetByIdAsync(item.AttributeId);
                if (attribute == null)
                {
                    throw new NotFoundException($"Атрибут с ID \"{item.AttributeId}\" не найден.");
                }
                product.AttributeValues.Add(new AttributeValue()
                {
                    AttributeId = item.AttributeId,
                    ValueString = item.ValueString,
                    ValueInt = item.ValueInt,
                    ValueNumeric = item.ValueNumeric
                });
            }

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return product;
        }
    }
}
