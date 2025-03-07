using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementVerifieeEventHandler(ILogger<PropositionEvenementVerifieeEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<PropositionEvenementVerifieeEvent>
{
    public override async Task HandleAsync(PropositionEvenementVerifieeEvent propositionEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence verification proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
        PropositionEvenementEntity entityToUpdate = await dbContext.PropositionEvenements.SingleOrDefaultAsync(pe => pe.Id == propositionEvent.PropositionEvenementId, cancellationToken)
            ?? throw new DataAccessException($"Aucune proposition evenement avec l'identifiant {propositionEvent.PropositionEvenementId} n'a été trouvé");
        entityToUpdate.PropositionStatut = PropositionStatut.Verifiee;
        logger.LogInformation("Fin Persistence verification proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
    }
}
