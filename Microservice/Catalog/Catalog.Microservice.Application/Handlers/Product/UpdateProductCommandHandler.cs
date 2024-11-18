using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Application.Service;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"Продукт с ID \"{request.Id}\" не найден.");
            }

            if ((product.Name != request.Name || product.BrandId != request.BrandId
                || product.CatalogId != request.CatalogId)
                && await _unitOfWork.Products.ExistProductByBrandId_CatalogId_Name(
               request.BrandId, request.CatalogId, request.Name))
            {
                throw new DataExistsException("Продукт с выбранным брендом, категорией и наименованием уже существует.");
            }

            if (await _unitOfWork.Brands.GetByIdAsync(request.BrandId) == null)
            {
                throw new NotFoundException($"Бренд с ID \"{request.BrandId}\" не найден.");
            }

            if (await _unitOfWork.Catalogs.GetByIdAsync(request.CatalogId) == null)
            {
                throw new NotFoundException($"Категория с ID \"{request.CatalogId}\" не найдена.");
            }

            product.Name = request.Name;
            product.Quantity = request.Quantity;
            product.BrandId = request.BrandId;
            product.CatalogId = request.CatalogId;

            if (request.Image != null)
            {
                product.ImageUrl = await _fileService.UploadFileAsync(request.Image, "images");
            }

            foreach (var item in request.ProductAttributes)
            {
                AttributeValue attributeValue = product.AttributeValues
                        .Where(x => x.AttributeId == item?.AttributeId)
                        .FirstOrDefault();
                if (attributeValue == null)
                {
                    product.AttributeValues.Add(new AttributeValue()
                    {
                        AttributeId = item.AttributeId,
                        ValueString = item.ValueString,
                        ValueInt = item.ValueInt,
                        ValueNumeric = item.ValueNumeric
                    });
                }
                else
                {
                    attributeValue.ValueString = item.ValueString;
                    attributeValue.ValueInt = item.ValueInt;
                    attributeValue.ValueNumeric = item.ValueNumeric;
                }
            }

            var itemsToRemove = new List<AttributeValue>();
            foreach (var item in product.AttributeValues)
            {
                if (!request.ProductAttributes.Any(x => x.AttributeId == item.AttributeId))
                    itemsToRemove.Add(item);
            }

            foreach (var item in itemsToRemove)
            {
                product.AttributeValues.Remove(item);
            }

            _unitOfWork.Products.Update(product);
            await _unitOfWork.CommitAsync();

            return product;
        }
    }
}
