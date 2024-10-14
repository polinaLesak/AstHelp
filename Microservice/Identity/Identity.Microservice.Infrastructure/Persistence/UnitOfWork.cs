using Identity.Microservice.Domain.Repositories;

namespace Identity.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }

        public UnitOfWork(
            EFDBContext context,
            IUserRepository users,
            IRoleRepository roles)
        {
            _context = context;
            Users = users;
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
