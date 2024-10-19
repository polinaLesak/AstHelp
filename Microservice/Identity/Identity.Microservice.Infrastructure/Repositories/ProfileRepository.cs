using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Microservice.Infrastructure.Repositories
{
    public class ProfileRepository : GenericRepository<Profile, int>, IProfileRepository
    {
        public ProfileRepository(EFDBContext context) : base(context) { }

        public async Task<Profile> GetByUserIdAsync(int userId)
        {
            return await _context.Profiles
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
