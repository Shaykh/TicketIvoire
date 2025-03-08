using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.LieuEvenements.Command;

public record ModifierLieuCommand(Guid LieuId, uint? Capacite, string Nom, string Description, string Adresse, string Ville) : ICommand;

public class ModifierLieuCommandValidator : AbstractValidator<ModifierLieuCommand>
{
    public ModifierLieuCommandValidator()
    {
        RuleFor(x => x.LieuId)
            .NotEmpty();
        RuleFor(cmd => cmd.Nom)
            .NotEmpty();
        RuleFor(cmd => cmd.Description)
            .NotEmpty();
        RuleFor(cmd => cmd.Ville)
            .NotEmpty();
        RuleFor(cmd => cmd.Adresse)
            .NotEmpty();
    }
}

public class ModifierLieuCommandHandler(ILogger<ModifierLieuCommandHandler> logger,
    IValidator<ModifierLieuCommand> validator,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork,
    ILieuRepository lieuRepository) : ICommandHandler<ModifierLieuCommand>
{
    public async Task Handle(ModifierLieuCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        logger.LogInformation("Command Modification d'un lieu {Command}", request);
        Lieu lieu = await lieuRepository.GetByIdAsync(request.LieuId);
        lieu.Modifier(request.Nom, request.Description, request.Adresse, request.Ville, request.Capacite);
        domainEventsContainer.AddEvents(lieu.DomainEvents);
        lieu.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Lieu modifié {LieuId}", lieu.Id.Value);
    }
}
