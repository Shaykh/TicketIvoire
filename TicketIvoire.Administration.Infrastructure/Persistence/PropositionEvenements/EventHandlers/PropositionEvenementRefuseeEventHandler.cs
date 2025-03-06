using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementRefuseeEventHandler : PersisterEventHandler<PropositionEvenementRefuseeEvent>
{
    public override Task HandleAsync(PropositionEvenementRefuseeEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
