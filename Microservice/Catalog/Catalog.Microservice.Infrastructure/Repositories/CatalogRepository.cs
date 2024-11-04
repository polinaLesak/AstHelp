using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class CatalogRepository : GenericRepository<Domain.Entities.Catalog, int>, ICatalogRepository
    {
        public CatalogRepository(EFDBContext context)
            : base(context) { }

        public override async Task<Domain.Entities.Catalog> GetByIdAsync(int id)
        {
            return await _context.Catalogs
                .Include(x => x.CatalogAttributes)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Domain.Entities.Catalog>> GetAllAsync()
        {
            return await _context.Catalogs
                .Include(x => x.CatalogAttributes)
                    .ThenInclude(x => x.Attribute)
                .ToListAsync();
        }

        public async Task<bool> ExistCatalogByName(string name)
        {
            return await _context.Catalogs
                .AnyAsync(x => x.Name == name);
        }
    }
}
