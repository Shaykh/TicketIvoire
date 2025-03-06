using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreDesactiveEventHandler : PersisterEventHandler<MembreDesactiveEvent>
{
    public override Task HandleAsync(MembreDesactiveEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
