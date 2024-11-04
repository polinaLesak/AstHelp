using System.Text.Json.Serialization;

namespace Catalog.Microservice.Domain.Entities
{
    /**
        AttributeType
        Содержит типы атрибутов для товаров. 
        (например, "String", "Numeric")
    **/
    public class AttributeType
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        [JsonIgnore]
        public ICollection<Attribute> Attributes { get; set; } = [];
    }
}
