namespace Cart.Microservice.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Carts { get; }
        ICartItemRepository CartItems { get; }

        Task<int> CommitAsync();
    }
}
