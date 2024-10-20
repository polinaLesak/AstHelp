using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
    }
}
