using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuRetireEventHandler : PersisterEventHandler<LieuRetireEvent>
{
    public override Task HandleAsync(LieuRetireEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
