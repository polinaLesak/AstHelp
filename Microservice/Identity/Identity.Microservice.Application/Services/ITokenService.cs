using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateMachineToken(string serviceName);
    }
}
