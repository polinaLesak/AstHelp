using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCatalogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await _unitOfWork.Catalogs.GetByIdAsync(request.CatalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Категория с ID \"{request.CatalogId}\" не найдена.");
            }

            try
            {
                _unitOfWork.Catalogs.Remove(catalog);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw new DataExistsException($"Данная категория с ID \"{request.CatalogId}\" используется. Удаление невозможно.");
            }

            return Unit.Value;
        }
    }
}
