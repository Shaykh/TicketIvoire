using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreCreeEventHandler : PersisterEventHandler<MembreCreeEvent>
{
    public override Task HandleAsync(MembreCreeEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
