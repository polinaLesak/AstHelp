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

        public ICollection<Attribute> Attributes { get; set; } = [];
    }
}
