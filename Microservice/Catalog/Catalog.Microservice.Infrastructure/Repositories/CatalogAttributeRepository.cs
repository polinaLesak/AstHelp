using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class CatalogAttributeRepository : GenericRepository<CatalogAttribute, int>, ICatalogAttributeRepository
    {
        public CatalogAttributeRepository(EFDBContext context)
            : base(context) { }
    }
}
