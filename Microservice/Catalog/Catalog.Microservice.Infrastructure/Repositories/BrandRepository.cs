using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand, int>, IBrandRepository
    {
        public BrandRepository(EFDBContext context)
            : base(context) { }

        public async Task<bool> ExistBrandByName(string name)
        {
            return await _context.Brands
                .AnyAsync(x => x.Name == name);
        }
    }
}
