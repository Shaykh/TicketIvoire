using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Application.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Application.Tests.Events;
#pragma warning disable CA1707
public class ServicesExtensionsTests
{
    [Fact]
    public void GivenServiceCollection_WhenAddEventsManagers_ThenAddEventsContainerAndDispatcher()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEventsManagers();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventsContainer) && s.ImplementationType == typeof(DomainEventsContainer) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventsDispatcher) && s.ImplementationType == typeof(DomainEventsDispatcher) && s.Lifetime == ServiceLifetime.Scoped);
    }
}
#pragma warning restore CA1707
