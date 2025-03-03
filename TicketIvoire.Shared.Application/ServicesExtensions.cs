using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Application.Events;

namespace TicketIvoire.Shared.Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddSharedApplication(this IServiceCollection services)
    {
        services.AddEventsManagers();

        return services;
    }
}
