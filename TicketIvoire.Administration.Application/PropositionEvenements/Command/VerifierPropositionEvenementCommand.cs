using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Command;

public record VerifierPropositionEvenementCommand(Guid PropositionEvenementId) : ICommand;

public class VerifierPropositionEvenementCommandValidator : AbstractValidator<VerifierPropositionEvenementCommand>
{
    public VerifierPropositionEvenementCommandValidator() => RuleFor(cmd => cmd.PropositionEvenementId).NotEmpty();
}

public class VerifierPropositionEvenementCommandHandler(ILogger<VerifierPropositionEvenementCommandHandler> logger,
    IValidator<VerifierPropositionEvenementCommand> validator,
    IPropositionEvenementRepository propositionEvenementRepository,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<VerifierPropositionEvenementCommand>
{
    public async Task Handle(VerifierPropositionEvenementCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        logger.LogInformation("Command Verifier Proposition Evenement {PropositionEvenementId}", command.PropositionEvenementId);
        PropositionEvenement propositionEvenement = await propositionEvenementRepository.GetByIdAsync(new PropositionEvenementId(command.PropositionEvenementId), cancellationToken);
        propositionEvenement.Verifier();
        domainEventsContainer.AddEvents(propositionEvenement.DomainEvents);
        propositionEvenement.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Proposition Evenement {PropositionEvenementId} vérifiée", command.PropositionEvenementId);
    }
}
