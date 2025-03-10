﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementRefuseeEventHandler(ILogger<PropositionEvenementRefuseeEventHandler> logger, AdministrationDbContext dbContext) : PersisterEventHandler<PropositionEvenementRefuseeEvent>
{
    public override async Task HandleAsync(PropositionEvenementRefuseeEvent propositionEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Persistence refus proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
        PropositionEvenementEntity entityToUpdate = await dbContext.PropositionEvenements.SingleOrDefaultAsync(pe => pe.Id == propositionEvent.PropositionEvenementId, cancellationToken)
            ?? throw new NotFoundException($"Aucune proposition evenement avec l'identifiant {propositionEvent.PropositionEvenementId} n'a été trouvé");
        var decision = PropositionDecision.PropositionRefusee(propositionEvent.UtilisateurId, propositionEvent.DateDecision, propositionEvent.Raisons);
        entityToUpdate.Decision = decision.ToEntity();
        logger.LogInformation("Fin Persistence refus proposition evenement {PropositionEvenementId}", propositionEvent.PropositionEvenementId);
    }
}
