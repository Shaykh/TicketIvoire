using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementCreeEventHandler(ILogger<PropositionEvenementCreeEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<PropositionEvenementCreeEvent>
{
    public override async Task HandleAsync(PropositionEvenementCreeEvent propositionEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence nouvelle proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
        PropositionEvenementEntity entity = propositionEvent.ToEntity();
        await dbContext.PropositionEvenements.AddAsync(entity, cancellationToken);
        logger.LogInformation("Fin Persistence nouvelle proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
    }
}
