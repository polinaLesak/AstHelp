using Identity.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Microservice.Infrastructure.Persistence
{
    public class EFDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MachineAccount> MachineAccounts { get; set; }

        public EFDBContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Системный администратор" },
                new Role { Id = 2, Name = "Ресурсный менеджер" },
                new Role { Id = 3, Name = "Сотрудник" }
                );
            modelBuilder.Entity<MachineAccount>().HasData(
                new MachineAccount { Id = 1, ServiceName = "Cart.Microservice", ApiKey = "9c125824-0c86-4c5e-b623-0765d998b441", CreatedAt = DateTime.UtcNow },
                new MachineAccount { Id = 2, ServiceName = "Catalog.Microservice", ApiKey = "c54c4162-d3ac-4297-b77d-89d888a2d688", CreatedAt = DateTime.UtcNow },
                new MachineAccount { Id = 3, ServiceName = "Orders.Microservice", ApiKey = "c9035c54-7b64-4980-8a13-e11213ad2b9f", CreatedAt = DateTime.UtcNow },
                new MachineAccount { Id = 4, ServiceName = "Notification.Microservice", ApiKey = "9aba0313-89bd-4aff-85c8-194ebc5e51dd", CreatedAt = DateTime.UtcNow }
                );

            modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
