using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreDesactiveEventHandler(ILogger<MembreDesactiveEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<MembreDesactiveEvent>
{
    public override async Task HandleAsync(MembreDesactiveEvent membreEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence désactivation membre {MembreId}", membreEvent.MembreId);
        MembreEntity entityToDeactive = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreEvent.MembreId, cancellationToken)
            ?? throw new DataAccessException($"Aucun membre avec l'identifiant {membreEvent.MembreId} n'a été trouvé");
        entityToDeactive.EstActif = false;
        logger.LogInformation("Fin Persistence désactivation membre {MembreId}", membreEvent.MembreId);
    }
}
