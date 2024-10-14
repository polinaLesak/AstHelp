namespace Identity.Microservice.Application.DTOs.Response
{
    public class UserLoginResponseDto
    {
        public string Username { get; set; } = "";
        public string JwtToken { get; set; } = "";
    }
}
