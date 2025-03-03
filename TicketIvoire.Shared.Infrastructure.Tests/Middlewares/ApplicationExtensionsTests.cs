using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Infrastructure.Middlewares;

namespace TicketIvoire.Shared.Infrastructure.Tests.Middlewares;

public class ApplicationExtensionsTests
{
    [Fact]
    public void GivenAddCorrelationId_WhenCalled_ThenReturnsApplicationBuilderWithCorrelationIdMiddleware()
    {
        // Arrange
        var services = new ServiceCollection();
        IApplicationBuilder applicationBuilder = new ApplicationBuilder(services.BuildServiceProvider(true));

        // Act
        IApplicationBuilder result = applicationBuilder.AddCorrelationId();

        // Assert
        Assert.IsType<CorrelationIdMiddleware>(result.Build().Target, exactMatch: false);
    }
}
