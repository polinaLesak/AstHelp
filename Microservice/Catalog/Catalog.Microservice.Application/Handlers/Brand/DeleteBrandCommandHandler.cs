using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(request.BrandId);
            if (brand == null)
            {
                throw new NotFoundException($"Бренд с ID \"{request.BrandId}\" не найден.");
            }

            try
            {
                _unitOfWork.Brands.Remove(brand);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw new DataExistsException($"Данный бренд с ID \"{request.BrandId}\" используется. Удаление невозможно.");
            }

            return Unit.Value;
        }
    }
}
