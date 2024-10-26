namespace Identity.Microservice.Application.DTOs.Response
{
    public class UserLoginResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string JwtToken { get; set; } = "";
        public int[] Roles { get; set; } = [];
    }
}
