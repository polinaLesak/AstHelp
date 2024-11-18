using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Models.Sorting;

namespace Catalog.Microservice.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<List<Product>> GetByIdsAsync(Guid[] ids);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllAsyncWithSorting(SortingRequest sortingRequest);
        Task<List<Product>> GetAllProductsByCatalogIdAsync(int categoryId);
        Task<bool> ExistProductByBrandId_CatalogId_Name(int brandId, int catalogId, string name);
    }
}
