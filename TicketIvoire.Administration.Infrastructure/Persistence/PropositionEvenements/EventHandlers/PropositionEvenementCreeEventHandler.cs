using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementCreeEventHandler : PersisterEventHandler<PropositionEvenementCreeEvent>
{
    public override Task HandleAsync(PropositionEvenementCreeEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
