using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Application;

namespace TicketIvoire.Administration.Application;

public static class ServicesExtensions
{
    public static ServiceCollection AddAdministrationApplication(this ServiceCollection services)
    {
        services.AddSharedApplication();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(ServicesExtensions).Assembly));
        return services;
    }
}
