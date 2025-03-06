using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementVerifieeEventHandler : PersisterEventHandler<PropositionEvenementVerifieeEvent>
{
    public override Task HandleAsync(PropositionEvenementVerifieeEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
