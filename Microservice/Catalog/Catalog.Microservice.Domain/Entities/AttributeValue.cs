using System.Text.Json.Serialization;

namespace Catalog.Microservice.Domain.Entities
{
    /**
        AttributeValue
        Содержит значения атрибутов для конкретных товаров
    **/
    public class AttributeValue
    {
        public Guid Id { get; set; }
        public int AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string? ValueString { get; set; }
        public int? ValueInt { get; set; }
        public decimal? ValueNumeric { get; set; }

        public Attribute Attribute { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
