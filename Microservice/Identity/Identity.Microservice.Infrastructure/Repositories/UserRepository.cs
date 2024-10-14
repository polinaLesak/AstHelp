﻿using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Identity.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Microservice.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(EFDBContext context) : base(context) { }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Username == username);
        }
    }
}
