namespace Catalog.Microservice.Application.DTOs.Request
{
    public class ProductAttributeRequestDto
    {
        public int AttributeId { get; set; }
        public string? ValueString { get; set; }
        public int? ValueInt { get; set; }
        public decimal? ValueNumeric { get; set; }
    }
}
