using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
    }
}
