using Microsoft.EntityFrameworkCore;

namespace Notification.Microservice.Infrastructure.Persistence
{
    public class EFDBContext : DbContext
    {

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
