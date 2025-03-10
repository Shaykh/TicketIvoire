using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuCoordonneesGeographiquesDefiniesEventHandler(ILogger<LieuCoordonneesGeographiquesDefiniesEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<LieuCoordonneesGeographiquesDefiniesEvent>
{
    public override async Task HandleAsync(LieuCoordonneesGeographiquesDefiniesEvent lieuEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence de la définition des coordonnées géographiques d'un lieu {LieuId}", lieuEvent.LieuId);
        LieuEntity entityToUpdate = await dbContext.Lieux.SingleOrDefaultAsync(l => l.Id == lieuEvent.LieuId, cancellationToken)
            ?? throw new NotFoundException($"Aucun lieu avec l'identifiant {lieuEvent.LieuId} n'a été trouvé");
        entityToUpdate.CoordonneesGeographiques = new Domain.LieuEvenements.LieuCoordonneesGeographiques(lieuEvent.Latitude, lieuEvent.Longitude);
        entityToUpdate.Update(Guid.Empty);
        logger.LogInformation("Fin Persistence de la définition des coordonnées géographiques d'un lieu {LieuId}", lieuEvent.LieuId);
    }
}
