using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Command;

public record AccepterPropositionEvenementCommand(Guid PropositionEvenementId, Guid UtilisateurId, DateTime DateDecision) : ICommand;

public class AccepterPropositionEvenementCommandHandler(ILogger<AccepterPropositionEvenementCommandHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<AccepterPropositionEvenementCommand>
{
    public async Task Handle(AccepterPropositionEvenementCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Accepter Proposition Evenement {PropositionEvenementId} by UtilisateurId : {UtilisateurId}", command.PropositionEvenementId, command.UtilisateurId);
        PropositionEvenement propositionEvenement = await propositionEvenementRepository.GetByIdAsync(new PropositionEvenementId(command.PropositionEvenementId));
        propositionEvenement.Accepter(new UtilisateurId(command.UtilisateurId), command.DateDecision);
        domainEventsContainer.AddEvents(propositionEvenement.DomainEvents);
        propositionEvenement.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Proposition Evenement {PropositionEvenementId} accepted by UtilisateurId : {UtilisateurId}", command.PropositionEvenementId, command.UtilisateurId);
    }
}
