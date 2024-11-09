using Microsoft.EntityFrameworkCore;

namespace Notification.Microservice.Infrastructure.Persistence
{
    public class EFDBContext : DbContext
    {
        public DbSet<Domain.Entities.Notification> Notifications { get; set; }

        public EFDBContext(DbContextOptions options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
