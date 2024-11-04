using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class AttributeRepository : GenericRepository<Domain.Entities.Attribute, int>, IAttributeRepository
    {
        public AttributeRepository(EFDBContext context)
            : base(context) { }

        public override async Task<IEnumerable<Domain.Entities.Attribute>> GetAllAsync()
        {
            return await _context.Attributes
                .Include(x => x.AttributeType)
                .ToListAsync();
        }

        public async Task<bool> ExistAttributeByName(string name)
        {
            return await _context.Attributes
                .AnyAsync(x => x.Name == name);
        }

        public async Task<List<Domain.Entities.Attribute>> GetAllAttributesByCatalogId(int catalogId)
        {
            return await _context.Attributes
                .Include(x => x.AttributeType)
                .Where(x => x.CatalogAttributes.Any(c => c.CatalogId == catalogId))
                .ToListAsync();
        }

        public async Task<bool> ExistAttributeById(int id)
        {
            return await _context.Attributes
                .AnyAsync(x => x.Id == id);
        }
    }
}
