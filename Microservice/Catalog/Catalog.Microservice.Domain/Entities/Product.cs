namespace Catalog.Microservice.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public int CatalogId { get; set; }
        public int BrandId { get; set; }

        public Catalog Catalog { get; set; } = null!;
        public Brand Brand { get; set; } = null!;
        public ICollection<AttributeValue> AttributeValues { get; set; } = [];
    }
}
