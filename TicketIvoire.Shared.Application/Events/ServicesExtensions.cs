using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Application.Events;

public static class ServicesExtensions
{
    public static IServiceCollection AddEventsManagers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventsContainer, DomainEventsContainer>();
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        return services;
    }
}
