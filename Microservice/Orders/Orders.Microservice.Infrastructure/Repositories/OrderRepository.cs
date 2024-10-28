﻿using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;

namespace Orders.Microservice.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Domain.Entities.Order, Guid>, IOrderRepository
    {
        public OrderRepository(EFDBContext context)
            : base(context) { }

        public async Task<int> CountByManagerIdAsync(int managerId)
        {
            return await _context.Orders
                .CountAsync(o => o.ManagerId == managerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders.AsNoTracking()
                .Include(x => x.Items)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByManagerIdAsync(int managerId)
        {
            return await _context.Orders.AsNoTracking()
                .Include(x => x.Items)
                .Where(x => x.ManagerId == managerId)
                .ToListAsync();
        }
    }
}