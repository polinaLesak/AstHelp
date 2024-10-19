using System.Text.Json.Serialization;

namespace Catalog.Microservice.Domain.Entities
{
    /**
        Attribute - список названий всех допустимых атрибутов, как
        "обороты" у стиралок, "объём ОЗУ" у ноутбуков 
    **/
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttributeTypeId { get; set; }

        public AttributeType AttributeType { get; set; }

        [JsonIgnore]
        public ICollection<CatalogAttribute> CatalogAttributes { get; set; } = [];
        [JsonIgnore]
        public ICollection<AttributeValue> AttributeValues { get; set; } = [];
    }
}
