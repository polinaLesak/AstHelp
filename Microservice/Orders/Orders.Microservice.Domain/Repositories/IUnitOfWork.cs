namespace Orders.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }

        Task<int> CommitAsync();
    }
}
