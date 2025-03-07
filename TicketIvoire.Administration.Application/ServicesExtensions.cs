using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Application;

namespace TicketIvoire.Administration.Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddAdministrationApplication(this IServiceCollection services)
    {
        services.AddSharedApplication();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(ServicesExtensions).Assembly));
        return services;
    }
}
