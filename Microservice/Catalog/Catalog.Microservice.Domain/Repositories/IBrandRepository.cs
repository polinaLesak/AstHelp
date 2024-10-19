using Catalog.Microservice.Domain.Entities;

namespace Catalog.Microservice.Domain.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand, int>
    {
        Task<bool> ExistBrandByName(string name);
    }
}
