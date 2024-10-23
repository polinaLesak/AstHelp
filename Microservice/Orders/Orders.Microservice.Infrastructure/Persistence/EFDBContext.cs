using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Infrastructure.Persistence
{
    public class EFDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public EFDBContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
