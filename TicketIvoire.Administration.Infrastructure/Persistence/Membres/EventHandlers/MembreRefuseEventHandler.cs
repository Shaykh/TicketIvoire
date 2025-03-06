using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreRefuseEventHandler : PersisterEventHandler<MembreRefuseEvent>
{
    public override Task HandleAsync(MembreRefuseEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
