using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class AttributeTypeRepository : GenericRepository<AttributeType, int>, IAttributeTypeRepository
    {
        public AttributeTypeRepository(EFDBContext context)
            : base(context)
        {
        }
    }
}
