using Microsoft.Extensions.DependencyInjection;
using Moq;
using TicketIvoire.Shared.Application.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Application.Tests.Events;

public class DomainEventsDispatcherTests
{
    [Fact]
    public async Task GivenDispatchAllNoTransactionalEventsAsync_WhenHandlersMatchEvents_ThenDispatchAllNoTransactionalEvents()
    {
        // Arrange
        var services = new ServiceCollection();
        var transactionalHandler = new TransactionalHandler();
        var nonTransactionalHandler = new NonTransactionalHandler();
        services.AddTransient<IDomainEventHandler<TestEvent>>(ctx => transactionalHandler);
        services.AddTransient<IDomainEventHandler<TestEvent>>(ctx => nonTransactionalHandler);
        var domainEventsDispatcher = new DomainEventsDispatcher(services.BuildServiceProvider(true));
        var domainEvents = new List<TestEvent>
        {
            new(),
            new()
        };

        // Act
        await domainEventsDispatcher.DispatchAllNoTransactionalEventsAsync(domainEvents, CancellationToken.None);

        // Assert
        Assert.False(transactionalHandler.HasBeenCalled);
        Assert.True(nonTransactionalHandler.HasBeenCalled);
    }

    [Fact]
    public async Task GivenDispatchAllNoTransactionalEventsAsync_WhenHandlersDoNotMatchEvents_ThenDoNotDispatchAllNoTransactionalEvents()
    {
        // Arrange
        var services = new ServiceCollection();
        var handlers = new List<IDomainEventHandler<IDomainEvent>>
        {
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask),
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask)
        };
        services.AddTransient(_ => handlers);
        var domainEventsDispatcher = new DomainEventsDispatcher(services.BuildServiceProvider(true));
        var domainEvents = new List<TestEvent>
        {
            new()
        };

        // Act
        await domainEventsDispatcher.DispatchAllNoTransactionalEventsAsync(domainEvents, CancellationToken.None);

        // Assert
        foreach (IDomainEventHandler<IDomainEvent> handler in handlers)
        {
            foreach (TestEvent domainEvent in domainEvents)
            {
                Mock.Get(handler).Verify(h => h.HandleAsync(domainEvent, CancellationToken.None), Times.Never);
            }
        }
    }

    [Fact]
    public async Task GivenDispatchAllNoTransactionalEventsAsync_WhenNoEvents_ThenDoNotDispatchAllNoTransactionalEvents()
    {
        // Arrange
        var services = new ServiceCollection();
        var handlers = new List<IDomainEventHandler<IDomainEvent>>
        {
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask),
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask)
        };
        services.AddTransient(_ => handlers);
        var domainEventsDispatcher = new DomainEventsDispatcher(services.BuildServiceProvider(true));
        var domainEvents = new List<TestEvent>();

        // Act
        await domainEventsDispatcher.DispatchAllNoTransactionalEventsAsync(domainEvents, CancellationToken.None);

        // Assert
        foreach (IDomainEventHandler<IDomainEvent> handler in handlers)
        {
            Mock.Get(handler).Verify(h => h.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }

    [Fact]
    public async Task GivenDispatchAllTransactionalEventsAsync_WhenHandlersMatchEvents_ThenDispatchAllTransactionalEvents()
    {
        // Arrange
        var services = new ServiceCollection();
        var transactionalHandler = new TransactionalHandler();
        var nonTransactionalHandler = new NonTransactionalHandler();
        services.AddTransient<IDomainEventHandler<TestEvent>>(ctx => transactionalHandler);
        services.AddTransient<IDomainEventHandler<TestEvent>>(ctx => nonTransactionalHandler);
        var domainEventsDispatcher = new DomainEventsDispatcher(services.BuildServiceProvider(true));
        var domainEvents = new List<TestEvent>
        {
            new(),
            new()
        };

        // Act
        await domainEventsDispatcher.DispatchAllTransactionalEventsAsync(domainEvents, CancellationToken.None);

        // Assert
        Assert.True(transactionalHandler.HasBeenCalled);
        Assert.False(nonTransactionalHandler.HasBeenCalled);
    }

    [Fact]
    public async Task GivenDispatchAllTransactionalEventsAsync_WhenHandlersDoNotMatchEvents_ThenDoNotDispatchAllTransactionalEvents()
    {
        // Arrange
        var services = new ServiceCollection();
        var handlers = new List<IDomainEventHandler<IDomainEvent>>
        {
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask),
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask)
        };
        services.AddTransient(_ => handlers);
        var domainEventsDispatcher = new DomainEventsDispatcher(services.BuildServiceProvider(true));
        var domainEvents = new List<TestEvent>
        {
            new()
        };

        // Act
        await domainEventsDispatcher.DispatchAllTransactionalEventsAsync(domainEvents, CancellationToken.None);

        // Assert
        foreach (IDomainEventHandler<IDomainEvent> handler in handlers)
        {
            foreach (TestEvent domainEvent in domainEvents)
            {
                Mock.Get(handler).Verify(h => h.HandleAsync(domainEvent, CancellationToken.None), Times.Never);
            }
        }
    }

    [Fact]
    public async Task GivenDispatchAllTransactionalEventsAsync_WhenNoEvents_ThenDoNotDispatchAllTransactionalEvents()
    {
        // Arrange
        var services = new ServiceCollection();
        var handlers = new List<IDomainEventHandler<IDomainEvent>>
        {
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask),
            Mock.Of<IDomainEventHandler<IDomainEvent>>(m => m.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()) == Task.CompletedTask)
        };
        services.AddTransient(_ => handlers);
        var domainEventsDispatcher = new DomainEventsDispatcher(services.BuildServiceProvider(true));
        var domainEvents = new List<TestEvent>();

        // Act
        await domainEventsDispatcher.DispatchAllTransactionalEventsAsync(domainEvents, CancellationToken.None);

        // Assert
        foreach (IDomainEventHandler<IDomainEvent> handler in handlers)
        {
            Mock.Get(handler).Verify(h => h.HandleAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }

    public sealed record TestEvent : IDomainEvent
    {
        public Guid Id => Guid.NewGuid();

        public DateTime CreatedAt => DateTime.UtcNow;
    }

    public sealed class NonTransactionalHandler : IDomainEventHandler<TestEvent>
    {
        public bool HasBeenCalled { get; set; }

        public Task HandleAsync(TestEvent domainEvent, CancellationToken cancellationToken)
        {
            HasBeenCalled = true;
            return Task.CompletedTask;
        }

        public bool IsTransactional() => false;
    }

    public sealed class TransactionalHandler : IDomainEventHandler<TestEvent>
    {
        public bool HasBeenCalled { get; set; }

        public Task HandleAsync(TestEvent domainEvent, CancellationToken cancellationToken)
        {
            HasBeenCalled = true;
            return Task.CompletedTask;
        }

        public bool IsTransactional() => true;
    }
}
