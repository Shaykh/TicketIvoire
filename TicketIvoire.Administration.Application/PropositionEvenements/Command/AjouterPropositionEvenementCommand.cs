using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Command;

public record AjouterPropositionEvenementCommand(Guid UtilisateurId, string Nom, string Description, DateTime DateDebut, DateTime DateFin, PropositionEvenementLieuDto Lieu) 
    : ICommand<Guid>;

public class AjouterPropositionEvenementCommandValidator : AbstractValidator<AjouterPropositionEvenementCommand>
{
    public AjouterPropositionEvenementCommandValidator()
    {
        RuleFor(cmd => cmd.UtilisateurId)
            .NotEmpty();
        RuleFor(cmd => cmd.Nom)
            .NotEmpty();
        RuleFor(cmd => cmd.Description)
            .NotEmpty();
        RuleFor(cmd => cmd)
            .Must(cmd => cmd.DateDebut < cmd.DateFin);
    }
}

public class AjouterPropositionEvenementCommandHandler(ILogger<AjouterPropositionEvenementCommandHandler> logger,
    IValidator<AjouterPropositionEvenementCommand> validator,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<AjouterPropositionEvenementCommand, Guid>
{
    public async Task<Guid> Handle(AjouterPropositionEvenementCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        logger.LogInformation("Command Ajouter Proposition Evenement by UtilisateurId : {UtilisateurId}", command.UtilisateurId);
        var propositionEvenement = PropositionEvenement.Create(new UtilisateurId(command.UtilisateurId), command.Nom, command.Description, command.DateDebut, command.DateFin, new PropositionLieu(command.Lieu.Nom, command.Lieu.Description, command.Lieu.Ville, command.Lieu.LieuEvenementId));
        domainEventsContainer.AddEvents(propositionEvenement.DomainEvents);
        propositionEvenement.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Proposition Evenement created with Id : {Id}", propositionEvenement.Id.Value);
        return propositionEvenement.Id.Value;
    }
}
