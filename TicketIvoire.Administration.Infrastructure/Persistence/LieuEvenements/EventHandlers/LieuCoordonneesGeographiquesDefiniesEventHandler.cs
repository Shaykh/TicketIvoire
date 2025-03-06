using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuCoordonneesGeographiquesDefiniesEventHandler : PersisterEventHandler<LieuCoordonneesGeographiquesDefiniesEvent>
{
    public override Task HandleAsync(LieuCoordonneesGeographiquesDefiniesEvent tEvent, CancellationToken cancellationToken) => throw new NotImplementedException();
}
