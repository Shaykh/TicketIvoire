using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementAccepteeEventHandler : PersisterEventHandler<PropositionEvenementAccepteeEvent>
{
    public override Task HandleAsync(PropositionEvenementAccepteeEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
