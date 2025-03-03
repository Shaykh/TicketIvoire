using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Shared.Infrastructure.Tests;

#pragma warning disable CA1707
public class ServicesExtensionsTests
{
    [Fact]
    public void GivenServiceCollection_WhenAddSharedInfrastructure_ThenAddUnitOfWork()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSharedInfrastructure();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IUnitOfWork) && s.ImplementationType == typeof(UnitOfWork) && s.Lifetime == ServiceLifetime.Scoped);
    }
}
#pragma warning restore CA1707
