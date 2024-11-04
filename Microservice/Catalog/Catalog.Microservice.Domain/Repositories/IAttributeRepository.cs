namespace Catalog.Microservice.Domain.Repositories
{
    public interface IAttributeRepository : IGenericRepository<Entities.Attribute, int>
    {
        Task<IEnumerable<Entities.Attribute>> GetAllAsync();
        Task<bool> ExistAttributeByName(string name);
        Task<bool> ExistAttributeById(int id);
        Task<List<Entities.Attribute>> GetAllAttributesByCatalogId(int catalogId);
    }
}
