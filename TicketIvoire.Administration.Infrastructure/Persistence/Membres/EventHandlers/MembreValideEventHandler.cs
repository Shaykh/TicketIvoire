using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreValideEventHandler : PersisterEventHandler<MembreValideEvent>
{
    public override Task HandleAsync(MembreValideEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
