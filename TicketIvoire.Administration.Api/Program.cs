using Carter;
using TicketIvoire.Administration.Api.Middlewares;
using TicketIvoire.Administration.Application;
using TicketIvoire.Administration.Infrastructure;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Api;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services
            .AddAdministrationApplication()
            .AddAdministrationInfrastructure(builder.Configuration);

        await builder.Services.ApplyMigrationAsync<AdministrationDbContext>(default);

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCarter();

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        WebApplication app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapCarter();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        await app.RunAsync();
    }
}
