using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class AttributeValueRepository : GenericRepository<AttributeValue, Guid>, IAttributeValueRepository
    {
        public AttributeValueRepository(EFDBContext context)
            : base(context) { }
    }
}
