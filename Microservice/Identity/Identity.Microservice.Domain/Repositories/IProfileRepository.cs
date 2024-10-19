using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Domain.Repositories
{
    public interface IProfileRepository : IGenericRepository<Profile, int>
    {
        Task<Profile> GetByUserIdAsync(int userId);
    }
}
