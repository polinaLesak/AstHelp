using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Brand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Brand> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(request.BrandId);
            if (brand == null)
            {
                throw new NotFoundException($"Бренд с ID \"{request.BrandId}\" не найден.");
            }

            if (await _unitOfWork.Brands.ExistBrandByName(request.Name))
            {
                throw new DataExistsException("Данный бренд уже существует.");
            }

            brand.Name = request.Name;

            _unitOfWork.Brands.Update(brand);
            await _unitOfWork.CommitAsync();

            return brand;
        }
    }
}
