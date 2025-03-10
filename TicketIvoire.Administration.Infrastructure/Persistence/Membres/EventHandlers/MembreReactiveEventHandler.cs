using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreReactiveEventHandler(ILogger<MembreReactiveEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<MembreReactiveEvent>
{
    public override async Task HandleAsync(MembreReactiveEvent membreEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence réactivation membre {MembreId}", membreEvent.MembreId);
        MembreEntity entityToDeactive = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreEvent.MembreId, cancellationToken)
            ?? throw new NotFoundException($"Aucun membre avec l'identifiant {membreEvent.MembreId} n'a été trouvé");
        entityToDeactive.EstActif = true;
        logger.LogInformation("Fin Persistence réactivation membre {MembreId}", membreEvent.MembreId);
    }
}
