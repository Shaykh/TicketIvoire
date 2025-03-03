using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using TicketIvoire.Shared.Infrastructure.Middlewares;

namespace TicketIvoire.Shared.Infrastructure.Tests.Middlewares;

public class CorrelationIdMiddlewareTests
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    [Fact]
    public async Task GivenMiddleware_WhenNoCorrelationIdOnRequest_ThenAddCorrelationIdToResponse()
    {
        // Arrange
        using IHost host = await new HostBuilder()
                .ConfigureWebHost(webBuilder => webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                        })
                        .Configure(app => app.AddCorrelationId()))
                .StartAsync();

        // Act
        HttpResponseMessage response = await host.GetTestClient().GetAsync("/");

        // Assert
        response.Headers.TryGetValues(CorrelationIdHeaderName, out IEnumerable<string>? correlationIds);
        Assert.NotNull(correlationIds);
        Assert.True(Guid.TryParse(correlationIds.FirstOrDefault(), out _));
    }

    [Fact]
    public async Task GivenMiddleware_WhenCorrelationIdOnRequest_ThenAddCorrelationIdToResponse()
    {
        // Arrange
        using IHost host = await new HostBuilder()
                .ConfigureWebHost(webBuilder => webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                        })
                        .Configure(app => app.AddCorrelationId()))
                .StartAsync();
        using HttpClient webTestClient = host.GetTestClient();
        using var request = new HttpRequestMessage()
        {
            RequestUri = new Uri("http://www.fakeURI.com"),
            Method = HttpMethod.Get
        };
        string expectedCorrelationId = Guid.NewGuid().ToString();
        request.Headers.Add(CorrelationIdHeaderName, expectedCorrelationId);

        // Act
        HttpResponseMessage response = await webTestClient.SendAsync(request);

        // Assert
        response.Headers.TryGetValues(CorrelationIdHeaderName, out IEnumerable<string>? correlationIds);
        Assert.NotNull(correlationIds);
        string? correlationId = correlationIds.FirstOrDefault();
        Assert.Same(expectedCorrelationId, correlationId);
    }
}
