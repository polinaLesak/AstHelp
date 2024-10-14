using System.Text.Json.Serialization;

namespace Identity.Microservice.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
