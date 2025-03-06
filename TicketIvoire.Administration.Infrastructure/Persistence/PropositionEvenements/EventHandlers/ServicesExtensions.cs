using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public static class ServicesExtensions
{
    internal static void AddPropositionEvenementPersisterEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventHandler<PropositionEvenementAccepteeEvent>, PropositionEvenementAccepteeEventHandler>();
        services.AddScoped<IDomainEventHandler<PropositionEvenementCreeEvent>, PropositionEvenementCreeEventHandler>();
        services.AddScoped<IDomainEventHandler<PropositionEvenementRefuseeEvent>, PropositionEvenementRefuseeEventHandler>();
        services.AddScoped<IDomainEventHandler<PropositionEvenementVerifieeEvent>, PropositionEvenementVerifieeEventHandler>();
    }
}
