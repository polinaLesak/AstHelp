using System.Text.Json.Serialization;

namespace Catalog.Microservice.Domain.Entities
{
    /**
        Catalog
        Содержит список категорий товаров (например, "Стиральные машины", "Ноутбуки")
    **/
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>();
        public ICollection<CatalogAttribute> CatalogAttributes { get; set; }
    }
}
