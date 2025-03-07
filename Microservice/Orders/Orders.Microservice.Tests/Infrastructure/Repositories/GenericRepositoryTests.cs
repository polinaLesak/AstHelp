using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Infrastructure.Persistence;
using Orders.Microservice.Infrastructure.Repositories;

namespace Orders.Microservice.Tests.Infrastructure.Repositories;

public class GenericRepositoryTests
{
    private readonly EFDBContext _context;
    private readonly GenericRepository<Order, Guid> _repository;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<EFDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new EFDBContext(options);
        _repository = new GenericRepository<Order, Guid>(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        var entity = new Order
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

        await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        Assert.NotNull(result);
        var orders = result.ToList();
        Assert.NotEmpty(orders);
        var order = orders.First();
        Assert.Equal(entity.Id, order.Id);
        Assert.Equal(entity.CustomerId, order.CustomerId);
        Assert.Equal(entity.CustomerFullname, order.CustomerFullname);
        Assert.Equal(entity.CustomerPosition, order.CustomerPosition);
        Assert.Equal(entity.ManagerId, order.ManagerId);
        Assert.Equal(entity.ManagerFullname, order.ManagerFullname);
        Assert.Equal(entity.ManagerPosition, order.ManagerPosition);
        Assert.Equal(entity.ReasonForIssue, order.ReasonForIssue);
        Assert.Equal(entity.CreatedAt, order.CreatedAt);
        Assert.Equal(entity.Status, order.Status);
        Assert.Equal(entity.Items.Count, order.Items.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntityById()
    {
        var id = Guid.NewGuid();
        var entity = new Order
        {
            Id = id,
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

        await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(entity.CustomerId, result.CustomerId);
        Assert.Equal(entity.CustomerFullname, result.CustomerFullname);
        Assert.Equal(entity.CustomerPosition, result.CustomerPosition);
        Assert.Equal(entity.ManagerId, result.ManagerId);
        Assert.Equal(entity.ManagerFullname, result.ManagerFullname);
        Assert.Equal(entity.ManagerPosition, result.ManagerPosition);
        Assert.Equal(entity.ReasonForIssue, result.ReasonForIssue);
        Assert.Equal(entity.CreatedAt, result.CreatedAt);
        Assert.Equal(entity.Status, result.Status);
        Assert.Equal(entity.Items.Count, result.Items.Count);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        var entities = new List<Order>
        {
            new Order {
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
            },
            new Order {
                Id = Guid.NewGuid(),
                CustomerId = 2,
                CustomerFullname = "Иванов Иван Иванович2",
                CustomerPosition = ".NET разработчик2",
                ManagerId = 4,
                ManagerFullname = "Иванов Григорий Иванович",
                ManagerPosition = "Ресурсный менеджер",
                ReasonForIssue = "Для работы",
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = []
            }
        };

        foreach (var entity in entities)
        {
            await _repository.AddAsync(entity);
        }

        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task FindAsync_ShouldReturnFilteredEntities()
    {
        await _repository.AddAsync(new Order {
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
        });
        await _repository.AddAsync(new Order {
            Id = Guid.NewGuid(),
            CustomerId = 1,
            CustomerFullname = "Иванов Иван Иванович",
            CustomerPosition = ".NET разработчик",
            ManagerId = 2,
            ManagerFullname = "Иванов Григорий Иванович",
            ManagerPosition = "Ресурсный менеджер",
            ReasonForIssue = "Для работы",
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Performed,
            Items = []
        });
        await _context.SaveChangesAsync();

        var result = await _repository.FindAsync(e => e.Status == OrderStatus.Performed);

        Assert.Single(result);
        Assert.NotNull(result.First());
        var entity = result.First();
        Assert.Equal(OrderStatus.Performed, entity.Status);
    }

    [Fact]
    public async Task Update_ShouldModifyEntity()
    {
        var id = Guid.NewGuid();
        var entity = new Order {
            Id = id,
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
        await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();

        entity.Status = OrderStatus.Performed;
        _repository.Update(entity);
        await _context.SaveChangesAsync();

        var updatedEntity = await _repository.GetByIdAsync(id);
        Assert.Equal(OrderStatus.Performed, updatedEntity.Status);
    }

    [Fact]
    public async Task Remove_ShouldDeleteEntity()
    {
        var id = Guid.NewGuid();
        var entity =  new Order {
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
        await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();

        _repository.Remove(entity);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(id);
        Assert.Null(result);
    }
}