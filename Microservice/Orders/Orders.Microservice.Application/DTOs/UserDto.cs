namespace Orders.Microservice.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public string Fullname { get; set; } = "";
        public string Position { get; set; } = "";
    }
}
