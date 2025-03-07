using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementAccepteeEventHandler(ILogger<PropositionEvenementAccepteeEvent> logger, AdministrationDbContext dbContext) : PersisterEventHandler<PropositionEvenementAccepteeEvent>
{
    public override async Task HandleAsync(PropositionEvenementAccepteeEvent propositionEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence validation proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
        PropositionEvenementEntity entityToUpdate = await dbContext.PropositionEvenements.SingleOrDefaultAsync(pe => pe.Id == propositionEvent.PropositionEvenementId, cancellationToken)
            ?? throw new DataAccessException($"Aucune proposition evenement avec l'identifiant {propositionEvent.PropositionEvenementId} n'a été trouvé");
        var decision = PropositionDecision.PropositionAcceptee(propositionEvent.UtilisateurId, propositionEvent.DateDecision);
        entityToUpdate.Decision = decision;
        logger.LogInformation("Fin Persistence validation proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
    }
}
