using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class AttributeRepository : GenericRepository<Domain.Entities.Attribute, int>, IAttributeRepository
    {
        public AttributeRepository(EFDBContext context)
            : base(context) { }

        public async Task<bool> ExistAttributeByName(string name)
        {
            return await _context.Attributes
                .AnyAsync(x => x.Name == name);
        }

        public async Task<List<Domain.Entities.Attribute>> GetAllAttributesByCatalogId(int catalogId)
        {
            return await _context.Attributes
                .Where(x => x.CatalogAttributes.Any(c => c.CatalogId == catalogId))
                .ToListAsync();
        }
    }
}
