using System.Text.Json.Serialization;

namespace Catalog.Microservice.Domain.Entities
{
    /**
        CatalogAttribute
        Связывает категории товаров с допустимыми атрибутами для этих категорий
    **/
    public class CatalogAttribute
    {
        public int CatalogId { get; set; }
        public int AttributeId { get; set; }

        [JsonIgnore]
        public Catalog Catalog { get; set; }
        public Attribute Attribute { get; set; }
    }
}
