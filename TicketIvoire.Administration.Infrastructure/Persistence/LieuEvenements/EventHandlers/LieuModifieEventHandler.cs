using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;

public class LieuModifieEventHandler(ILogger<LieuModifieEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<LieuModifieEvent>
{
    public override async Task HandleAsync(LieuModifieEvent lieuEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence de la modification d'un nouveau lieu {LieuId}", lieuEvent.LieuId);
        LieuEntity entityToUpdate = await dbContext.Lieux.SingleOrDefaultAsync(l => l.Id == lieuEvent.LieuId, cancellationToken)
            ?? throw new NotFoundException($"Aucun lieu avec l'identifiant {lieuEvent.LieuId} n'a été trouvé");
        entityToUpdate.UpdateTo(lieuEvent);
        logger.LogInformation("Fin Persistence de la modification d'un nouveau lieu {LieuId}", lieuEvent.LieuId);
    }
}
