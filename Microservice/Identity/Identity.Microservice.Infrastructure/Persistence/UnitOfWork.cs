using Identity.Microservice.Domain.Repositories;

namespace Identity.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;
        public IUserRepository Users { get; }
        public IProfileRepository Profiles { get; }
        public IRoleRepository Roles { get; }
        public IMachineAccountRepository MachineAccounts { get; }

        public UnitOfWork(
            EFDBContext context,
            IUserRepository users,
            IProfileRepository prosiles,
            IRoleRepository roles,
            IMachineAccountRepository machineAccounts)
        {
            _context = context;
            Users = users;
            Profiles = prosiles;
            Roles = roles;
            MachineAccounts = machineAccounts;
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
