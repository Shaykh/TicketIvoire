using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuAjouteEventHandler : PersisterEventHandler<LieuAjouteEvent>
{
    public override Task HandleAsync(LieuAjouteEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
