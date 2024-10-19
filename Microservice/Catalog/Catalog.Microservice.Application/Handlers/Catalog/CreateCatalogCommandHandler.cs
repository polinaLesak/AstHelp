using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
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
                throw new DataExistsException("Данная категория уже существует.");
            }

            var catalog = new Domain.Entities.Catalog
            {
                Name = request.Name
            };

            await _unitOfWork.Catalogs.AddAsync(catalog);
            await _unitOfWork.CommitAsync();

            return catalog;
        }
    }
}
