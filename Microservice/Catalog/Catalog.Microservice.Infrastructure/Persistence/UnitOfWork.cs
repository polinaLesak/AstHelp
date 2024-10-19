using Catalog.Microservice.Domain.Repositories;

namespace Catalog.Microservice.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;

        public IAttributeRepository Attributes { get; }
        public IAttributeTypeRepository AttributeTypes { get; }
        public IAttributeValueRepository AttributeValues { get; }
        public IBrandRepository Brands { get; }
        public ICatalogAttributeRepository CatalogAttributes { get; }
        public ICatalogRepository Catalogs { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(
            EFDBContext context,
            IAttributeRepository attributes,
            IAttributeTypeRepository attributeTypes,
            IAttributeValueRepository attributeValues,
            IBrandRepository brands,
            ICatalogAttributeRepository catalogAttributes,
            ICatalogRepository catalogs,
            IProductRepository products)
        {
            _context = context;
            Attributes = attributes;
            AttributeTypes = attributeTypes;
            AttributeValues = attributeValues;
            Brands = brands;
            CatalogAttributes = catalogAttributes;
            Catalogs = catalogs;
            Products = products;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
