using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuAjouteEventHandler(ILogger<LieuAjouteEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<LieuAjouteEvent>
{
    public override async Task HandleAsync(LieuAjouteEvent lieuEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence de l'ajout d'un nouveau lieu {LieuId}", lieuEvent.LieuId);
        LieuEntity entity = lieuEvent.ToEntity();
        await dbContext.Lieux.AddAsync(entity, cancellationToken);
        logger.LogInformation("Fin Persistence de l'ajout d'un nouveau lieu {LieuId}", lieuEvent.LieuId);
    }
}
