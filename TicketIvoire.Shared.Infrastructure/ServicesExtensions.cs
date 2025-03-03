using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Shared.Infrastructure;

public static class ServicesExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
