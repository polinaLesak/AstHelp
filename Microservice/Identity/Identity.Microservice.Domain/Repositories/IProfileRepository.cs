using Identity.Microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Microservice.Domain.Repositories
{
    public interface IProfileRepository : IGenericRepository<Profile, int>
    {
        Task<Profile> GetByUserIdAsync(int userId);
    }
}
