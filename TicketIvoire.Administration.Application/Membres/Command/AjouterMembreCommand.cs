using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Membres.Command;

public record AjouterMembreCommand(string Login, string Email, string Nom, string Prenom, string Telephone, DateTime DateAdhesion) : ICommand<Guid>;

public class AjouterMembreCommandValidator : AbstractValidator<AjouterMembreCommand>
{
    public AjouterMembreCommandValidator()
    {
        RuleFor(cmd => cmd.Login)
            .NotEmpty();
        RuleFor(cmd => cmd.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(cmd => cmd.Nom)
            .NotEmpty();
        RuleFor(cmd => cmd.Prenom)  
            .NotEmpty();
        RuleFor(cmd => cmd.Telephone)
            .NotEmpty();
    }
}

public class AjouterMembreCommandHandler(ILogger<AjouterMembreCommandHandler> logger,
    IValidator<AjouterMembreCommand> validator,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<AjouterMembreCommand, Guid>
{
    public async Task<Guid> Handle(AjouterMembreCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        logger.LogInformation("Command Add Membre {Command}", command);
        var membre = Membre.Create(command.Login, command.Email, command.Nom, command.Prenom, command.Telephone, command.DateAdhesion);
        domainEventsContainer.AddEvents(membre.DomainEvents);
        membre.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Membre {MembreId} created", membre.Id);
        return membre.Id.Value;
    }
}
