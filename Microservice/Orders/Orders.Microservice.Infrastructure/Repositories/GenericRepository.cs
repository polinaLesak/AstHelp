﻿using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Orders.Microservice.Infrastructure.Repositories
{
    public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class
    {
        protected readonly EFDBContext _context;

        public GenericRepository(EFDBContext context)
        {
            _context = context;
        }

        public async virtual Task<T> GetByIdAsync(K id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
