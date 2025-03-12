using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using TicketIvoire.Administration.Api.Middlewares;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Administration.Api.Tests.Middlewares;

public class GlobalExceptionHandlerTests
{
    private readonly IHostBuilder _hostBuilder;

    public GlobalExceptionHandlerTests()
    {
        _hostBuilder = new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddProblemDetails();
                        services.AddExceptionHandler<GlobalExceptionHandler>();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseExceptionHandler();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/ok", () =>
                            {
                                TypedResults.Text("Endpoint sans erreur!");
                            });
                            endpoints.MapGet("/notFound", () =>
                            {
                                throw new NotFoundException("0", "fakeEntity");
                            });
                            endpoints.MapGet("/badrequest", () =>
                            {
                                throw new BadRequestException("Bad request");
                            });
                            endpoints.MapGet("/validationerror", () =>
                            {
                                throw new ValidationException("Bad request");
                            });
                            endpoints.MapGet("/brokenbusinessrule", () =>
                            {
                                throw new BrokenBusinessRuleException(new Mock<IBusinessRule>().Object);
                            });
                            endpoints.MapGet("/internalservererror", () =>
                            {
                                throw new NotImplementedException();
                            });
                        });
                    });
            });
    }

    [Theory]
    [InlineData("/notFound", HttpStatusCode.NotFound)]
    [InlineData("/badrequest", HttpStatusCode.BadRequest)]
    [InlineData("/validationerror", HttpStatusCode.BadRequest)]
    [InlineData("/brokenbusinessrule", HttpStatusCode.Forbidden)]
    [InlineData("/internalservererror", HttpStatusCode.InternalServerError)]
    public async Task Handle_ShouldHandleExceptionAndReturnProblemDetails(string url, HttpStatusCode statusCode)
    {
        using var host = await _hostBuilder
            .StartAsync();
        var client = host.GetTestClient();
        var response = await client.GetAsync("/ok");
        var notFoundResponse = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(statusCode, notFoundResponse.StatusCode);
    }
}
