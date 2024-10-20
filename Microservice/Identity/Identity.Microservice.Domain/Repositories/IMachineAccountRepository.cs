using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Domain.Repositories
{
    public interface IMachineAccountRepository : IGenericRepository<MachineAccount, int>
    {
        Task<MachineAccount> GetByApiKeyAsync(string apiKey);
    }
}
