using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreValideEventHandler(ILogger<MembreValideEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<MembreValideEvent>
{
    public override async Task HandleAsync(MembreValideEvent membreEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence validation membre {MembreId}", membreEvent.MembreId);
        MembreEntity entityToDeactive = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreEvent.MembreId, cancellationToken)
            ?? throw new NotFoundException($"Aucun membre avec l'identifiant {membreEvent.MembreId} n'a été trouvé");
        entityToDeactive.StatutAdhesion = Domain.Membres.StatutAdhesion.Accepte;
        logger.LogInformation("Fin Persistence validation membre {MembreId}", membreEvent.MembreId);
    }
}
