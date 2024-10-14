using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Domain.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role, int>
    {
        Task<Role> GetByNameAsync(string name);
    }
}
