using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Models.Sorting;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Persistence;
using Catalog.Microservice.Infrastructure.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Microservice.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product, Guid>, IProductRepository
    {
        public ProductRepository(EFDBContext context)
            : base(context) { }

        public override async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Catalog)
                .Include(x => x.AttributeValues)
                    .ThenInclude(x => x.Attribute)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetByIdsAsync(Guid[] ids)
        {
            return await _context.Products.AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Catalog)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Catalog)
                .Include(x => x.AttributeValues)
                    .ThenInclude(x => x.Attribute)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsyncWithSorting(SortingRequest sortingRequest)
        {
            return await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Catalog)
                .Include(x => x.AttributeValues)
                    .ThenInclude(x => x.Attribute)
                .SortByField(sortingRequest)
                .ToListAsync();
        }

        public async Task<bool> ExistProductByBrandId_CatalogId_Name(int brandId, int catalogId, string name)
        {
            return await _context.Products
                .AnyAsync(x => x.BrandId == brandId
                    && x.CatalogId == catalogId
                    && x.Name == name);
        }

        public async Task<List<Product>> GetAllProductsByCatalogIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(x => x.CatalogId == categoryId)
                .ToListAsync();
        }
    }
}
