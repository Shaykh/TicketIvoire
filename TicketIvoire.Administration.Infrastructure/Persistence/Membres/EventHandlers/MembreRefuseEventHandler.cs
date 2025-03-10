using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;

public class MembreRefuseEventHandler(ILogger<MembreRefuseEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<MembreRefuseEvent>
{
    public override async Task HandleAsync(MembreRefuseEvent membreEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence refus membre {MembreId}", membreEvent.MembreId);
        MembreEntity entityToDeactive = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreEvent.MembreId, cancellationToken)
            ?? throw new NotFoundException($"Aucun membre avec l'identifiant {membreEvent.MembreId} n'a été trouvé");
        entityToDeactive.StatutAdhesion = Domain.Membres.StatutAdhesion.Refuse;
        logger.LogInformation("Fin Persistence refus membre {MembreId}", membreEvent.MembreId);
    }
}
