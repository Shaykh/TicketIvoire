using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public static class ServicesExtensions
{
    internal static void AddLieuPersisterEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventHandler<LieuAjouteEvent>, LieuAjouteEventHandler>();
        services.AddScoped<IDomainEventHandler<LieuCoordonneesGeographiquesDefiniesEvent>, LieuCoordonneesGeographiquesDefiniesEventHandler>();
        services.AddScoped<IDomainEventHandler<LieuModifieEvent>, LieuModifieEventHandler>();
        services.AddScoped<IDomainEventHandler<LieuRetireEvent>, LieuRetireEventHandler>();
    }
}
