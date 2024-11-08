namespace Orders.Microservice.Application.Service
{
    public interface ICartService
    {
        Task ResetCartByUserId(int userId);
    }
}
