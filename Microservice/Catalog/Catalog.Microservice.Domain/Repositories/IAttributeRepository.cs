namespace Catalog.Microservice.Domain.Repositories
{
    public interface IAttributeRepository : IGenericRepository<Entities.Attribute, int>
    {
        Task<bool> ExistAttributeByName(string name);
        Task<List<Entities.Attribute>> GetAllAttributesByCatalogId(int catalogId);
    }
}
