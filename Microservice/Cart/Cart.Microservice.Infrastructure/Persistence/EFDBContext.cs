using Cart.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Microservice.Infrastructure.Persistence
{
    public class EFDBContext : DbContext
    {
        public DbSet<Domain.Entities.Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public EFDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId);
        }
    }
}
