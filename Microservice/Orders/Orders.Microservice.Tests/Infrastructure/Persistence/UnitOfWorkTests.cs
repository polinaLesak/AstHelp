using Microsoft.EntityFrameworkCore;
using Moq;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using Orders.Microservice.Infrastructure.Persistence;

namespace Orders.Microservice.Tests.Infrastructure.Persistence;

public class UnitOfWorkTests
{
    private readonly EFDBContext _context;
    private readonly UnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<EFDBContext>()
            .UseInMemoryDatabase(databaseName: "UnitOfWorkTestDB")
            .Options;

        _context = new EFDBContext(options);
        Mock<IOrderRepository> mockOrderRepo = new();

        _unitOfWork = new UnitOfWork(_context, mockOrderRepo.Object, null);
    }

    [Fact]
    public async Task CommitAsyncWithoutSavingData_ShouldCallSaveChangesAsyncWithZeroResult()
    {
        var result = await _unitOfWork.CommitAsync();

        Assert.Equal(0, result);
    }

    [Fact]
    public async Task CommitAsyncWithSavingData_ShouldCallSaveChangesAsyncWithNonZeroResult()
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = 1,
            CustomerFullname = "Иванов Иван Иванович",
            CustomerPosition = ".NET разработчик",
            ManagerId = 2,
            ManagerFullname = "Иванов Григорий Иванович",
            ManagerPosition = "Ресурсный менеджер",
            ReasonForIssue = "Для работы",
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            Items = []
        };
    
        _context.Entry(order).State = EntityState.Added;
        await _unitOfWork.Orders.AddAsync(order);

        var result = await _unitOfWork.CommitAsync();

        Assert.Equal(1, result);
    }

    [Fact]
    public void Dispose_ShouldCallDisposeOnContext()
    {
        _unitOfWork.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _context.Orders.ToList());
    }
}