using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuModifieEventHandler : PersisterEventHandler<LieuModifieEvent>
{
    public override Task HandleAsync(LieuModifieEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
