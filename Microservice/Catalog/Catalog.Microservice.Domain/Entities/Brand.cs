using System.Text.Json.Serialization;

namespace Catalog.Microservice.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = [];
    }
}
