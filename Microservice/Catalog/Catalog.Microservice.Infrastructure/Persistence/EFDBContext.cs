using Catalog.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Microservice.Infrastructure.Persistence
{
    public class EFDBContext : DbContext
    {
        public DbSet<Domain.Entities.Catalog> Catalogs { get; set; }
        public DbSet<Domain.Entities.Attribute> Attributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<CatalogAttribute> CatalogAttributes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }

        public EFDBContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AttributeType>().HasData(
                new AttributeType { Id = 1, Name = "String" },
                new AttributeType { Id = 2, Name = "Integer" },
                new AttributeType { Id = 3, Name = "Numeric" }
                );

            // Категория и продукты
            modelBuilder.Entity<Domain.Entities.Catalog>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Catalog)
                .HasForeignKey(p => p.CatalogId);

            // Бренд и продукты
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Products)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.BrandId);

            // Категория и атрибуты через CatalogAttribute
            modelBuilder.Entity<CatalogAttribute>()
                .HasKey(ca => new { ca.CatalogId, ca.AttributeId });

            modelBuilder.Entity<CatalogAttribute>()
                .HasOne(ca => ca.Catalog)
                .WithMany(c => c.CatalogAttributes)
                .HasForeignKey(ca => ca.CatalogId);

            modelBuilder.Entity<CatalogAttribute>()
                .HasOne(ca => ca.Attribute)
                .WithMany(a => a.CatalogAttributes)
                .HasForeignKey(ca => ca.AttributeId);

            // Продукт и атрибуты через AttributeValue
            modelBuilder.Entity<AttributeValue>()
                .HasOne(av => av.Product)
                .WithMany(p => p.AttributeValues)
                .HasForeignKey(av => av.ProductId);

            modelBuilder.Entity<AttributeValue>()
                .HasOne(av => av.Attribute)
                .WithMany(a => a.AttributeValues)
                .HasForeignKey(av => av.AttributeId);
        }
    }
}
