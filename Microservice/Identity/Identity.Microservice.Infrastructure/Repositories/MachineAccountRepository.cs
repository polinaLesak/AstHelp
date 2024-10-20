using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Microservice.Infrastructure.Repositories
{
    public class MachineAccountRepository : GenericRepository<MachineAccount, int>, IMachineAccountRepository
    {
        public MachineAccountRepository(EFDBContext context) : base(context) { }

        public async Task<MachineAccount> GetByApiKeyAsync(string apiKey)
        {
            return await _context.MachineAccounts
                .FirstOrDefaultAsync(ma => ma.ApiKey == apiKey);
        }
    }
}
