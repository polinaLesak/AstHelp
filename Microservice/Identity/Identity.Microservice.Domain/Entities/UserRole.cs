using System.Text.Json.Serialization;

namespace Identity.Microservice.Domain.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
