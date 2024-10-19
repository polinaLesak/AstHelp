using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Brand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Brands.ExistBrandByName(request.Name))
            {
                throw new DataExistsException("Данный бренд уже существует.");
            }

            var brand = new Brand
            {
                Name = request.Name
            };

            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.CommitAsync();

            return brand;
        }
    }
}
