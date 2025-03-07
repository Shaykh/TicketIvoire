using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreCreeEventHandler(ILogger<MembreCreeEvent> logger, AdministrationDbContext dbContext) : PersisterEventHandler<MembreCreeEvent>
{
    public override async Task HandleAsync(MembreCreeEvent membreEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence d'un nouveau membre {MembreId}", membreEvent.MembreId);
        MembreEntity entity = membreEvent.ToEntity();
        await dbContext.Membres.AddAsync(entity, cancellationToken);
        logger.LogInformation("Fin Persistence d'un nouveau membre {MembreId}", membreEvent.MembreId);
    }
}
