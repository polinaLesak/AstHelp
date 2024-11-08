namespace Cart.Microservice.Application.Service
{
    public interface IMachineTokenService
    {
        Task<string> GetTokenAsync();
    }
}
