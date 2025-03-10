using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuRetireEventHandler(ILogger<LieuRetireEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<LieuRetireEvent>
{
    public override async Task HandleAsync(LieuRetireEvent lieuEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence retrait lieu {LieuId}", lieuEvent.LieuId);
        LieuEntity entityToDelete = await dbContext.Lieux.SingleOrDefaultAsync(l => l.Id == lieuEvent.LieuId, cancellationToken)
            ?? throw new NotFoundException($"Aucun lieu avec l'identifiant {lieuEvent.LieuId} n'a été trouvé");
        entityToDelete.RaisonsRetrait = lieuEvent.Raisons;
        entityToDelete.Delete(lieuEvent.UtilisateurId);
        logger.LogInformation("Fin Persistence retrait lieu {LieuId}", lieuEvent.LieuId);
    }
}
