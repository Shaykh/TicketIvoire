using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public static class ServicesExtensions
{
    internal static void AddMembrePersisterEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventHandler<MembreCreeEvent>, MembreCreeEventHandler>();
        services.AddScoped<IDomainEventHandler<MembreDesactiveEvent>, MembreDesactiveEventHandler>();
        services.AddScoped<IDomainEventHandler<MembreReactiveEvent>, MembreReactiveEventHandler>();
        services.AddScoped<IDomainEventHandler<MembreRefuseEvent>, MembreRefuseEventHandler>();
        services.AddScoped<IDomainEventHandler<MembreValideEvent>, MembreValideEventHandler>();
    }
}
