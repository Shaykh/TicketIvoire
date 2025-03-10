using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Membres.Command;

public record ValiderMembreCommand(Guid MembreId) : ICommand;

public class ValiderMembreCommandValidator : AbstractValidator<ValiderMembreCommand>
{
    public ValiderMembreCommandValidator() => RuleFor(cmd => cmd.MembreId).NotEmpty();
}

public class ValiderMembreCommandHandler(ILogger<ValiderMembreCommandHandler> logger,
    IValidator<ValiderMembreCommand> validator,
    IDomainEventsContainer domainEventsContainer,
    IMembreRepository membreRepository, 
    IUnitOfWork unitOfWork) : ICommandHandler<ValiderMembreCommand>
{
    public async Task Handle(ValiderMembreCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        logger.LogInformation("Command Valider Membre {MembreId}", command.MembreId);
        Membre membre = await membreRepository.GetByIdAsync(new MembreId(command.MembreId), cancellationToken);
        membre.Valider();
        domainEventsContainer.AddEvents(membre.DomainEvents);
        membre.ClearEvents();
        logger.LogInformation("Membre {MembreId} validated", membre.Id);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
