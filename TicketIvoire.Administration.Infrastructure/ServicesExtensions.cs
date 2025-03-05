using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Infrastructure;

namespace TicketIvoire.Administration.Infrastructure;

public static class ServicesExtensions
{
    public static IServiceCollection AddAdministrationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure();
        services.AddAdministrationPersistence(configuration);
        return services;
    }
}
