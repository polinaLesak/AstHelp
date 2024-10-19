namespace Catalog.Microservice.Domain.Repositories
{
    public interface ICatalogRepository : IGenericRepository<Entities.Catalog, int>
    {
        Task<Entities.Catalog> GetByIdAsync(int id);
        Task<bool> ExistCatalogByName(string name);
    }
}
