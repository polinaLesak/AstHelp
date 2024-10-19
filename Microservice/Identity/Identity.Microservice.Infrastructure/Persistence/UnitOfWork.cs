using Identity.Microservice.Domain.Repositories;

namespace Identity.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;
        public IUserRepository Users { get; }
        public IProfileRepository Profiles { get; }
        public IRoleRepository Roles { get; }

        public UnitOfWork(
            EFDBContext context,
            IUserRepository users,
            IProfileRepository prosiles,
            IRoleRepository roles)
        {
            _context = context;
            Users = users;
            Profiles = prosiles;
            Roles = roles;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
