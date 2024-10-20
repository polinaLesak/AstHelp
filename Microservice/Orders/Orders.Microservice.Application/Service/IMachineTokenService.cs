namespace Orders.Microservice.Application.Service
{
    public interface IMachineTokenService
    {
        Task<string> GetTokenAsync();
    }
}
