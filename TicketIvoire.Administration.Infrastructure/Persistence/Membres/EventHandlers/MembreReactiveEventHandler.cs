using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreReactiveEventHandler : PersisterEventHandler<MembreReactiveEvent>
{
    public override Task HandleAsync(MembreReactiveEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
