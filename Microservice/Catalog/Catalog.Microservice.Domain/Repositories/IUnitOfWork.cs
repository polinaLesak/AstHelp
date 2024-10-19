namespace Catalog.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAttributeRepository Attributes { get; }
        IAttributeTypeRepository AttributeTypes { get; }
        IAttributeValueRepository AttributeValues { get; }
        IBrandRepository Brands { get; }
        ICatalogAttributeRepository CatalogAttributes { get; }
        ICatalogRepository Catalogs { get; }
        IProductRepository Products { get; }

        Task<int> CommitAsync();
    }
}
