using Orders.Microservice.Application.DTOs;

namespace Orders.Microservice.Application.Service
{
    public interface IIdentityService
    {
        Task<List<UserInfo>> GetAllManagersAsync();
        Task<UserInfo> GetUserInfoById(int userId);
    }
}
