using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Microservice.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(EFDBContext context) : base(context)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
