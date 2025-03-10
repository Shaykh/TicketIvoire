using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Command;

public record RefuserPropositionEvenementCommand(Guid PropositionEvenementId, Guid UtilisateurId, DateTime DateDecision, string Raisons) : ICommand;

public class RefuserPropositionEvenementCommandValidator : AbstractValidator<RefuserPropositionEvenementCommand>
{
    public RefuserPropositionEvenementCommandValidator()
    {
        RuleFor(cmd => cmd.PropositionEvenementId)
            .NotEmpty();
        RuleFor(cmd => cmd.UtilisateurId)
            .NotEmpty();
        RuleFor(cmd => cmd.Raisons)
            .NotEmpty();
    }
}

public class RefuserPropositionEvenementCommandHandler(ILogger<RefuserPropositionEvenementCommandHandler> logger,
    IValidator<RefuserPropositionEvenementCommand> validator,
    IPropositionEvenementRepository propositionEvenementRepository,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<RefuserPropositionEvenementCommand>
{
    public async Task Handle(RefuserPropositionEvenementCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        logger.LogInformation("Command Refuser Proposition Evenement {PropositionEvenementId} by UtilisateurId : {UtilisateurId}", command.PropositionEvenementId, command.UtilisateurId);
        PropositionEvenement propositionEvenement = await propositionEvenementRepository.GetByIdAsync(new PropositionEvenementId(command.PropositionEvenementId));
        propositionEvenement.Refuser(new UtilisateurId(command.UtilisateurId), command.DateDecision, command.Raisons);
        domainEventsContainer.AddEvents(propositionEvenement.DomainEvents);
        propositionEvenement.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Proposition Evenement {PropositionEvenementId} refusée", command.PropositionEvenementId);
    }
}
