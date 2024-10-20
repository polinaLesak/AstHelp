using Orders.Microservice.Application.DTOs;

namespace Orders.Microservice.Application.Service
{
    public interface IIdentityService
    {
        Task<List<UserDto>> GetAllManagersAsync();
    }
}
