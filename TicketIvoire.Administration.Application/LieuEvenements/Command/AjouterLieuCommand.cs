using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.LieuEvenements.Command;

public record AjouterLieuCommand(uint? Capacite, string Nom, string Description, string Adresse, string Ville) : ICommand<Guid>;

public class AjouterLieuCommandValidator : AbstractValidator<AjouterLieuCommand>
{
    public AjouterLieuCommandValidator()
    {
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

public class AjouterLieuCommandHandler(ILogger<AjouterLieuCommandHandler> logger,
    IValidator<AjouterLieuCommand> validator,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<AjouterLieuCommand, Guid>
{
    public async Task<Guid> Handle(AjouterLieuCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        logger.LogInformation("Command Ajout d'un lieu {Command}", request);
        var lieu = Lieu.Create(request.Nom, request.Description, request.Adresse, request.Ville, request.Capacite);
        domainEventsContainer.AddEvents(lieu.DomainEvents);
        lieu.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Lieu ajouté {LieuId}", lieu.Id.Value);
        return lieu.Id.Value;
    }
}
