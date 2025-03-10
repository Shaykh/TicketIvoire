using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Membres.Command;

public record DesactiverMembreCommand(Guid MembreId, string Raisons) : ICommand;

public class DesactiverMembreCommandValidator : AbstractValidator<DesactiverMembreCommand>
{
    public DesactiverMembreCommandValidator()
    {
        RuleFor(cmd => cmd.MembreId)
            .NotEmpty();
        RuleFor(cmd => cmd.Raisons)
            .NotEmpty();
    }
}

public class DesactiverMembreCommandHandler(ILogger<DesactiverMembreCommandHandler> logger,
    IValidator<DesactiverMembreCommand> validator,
    IMembreRepository membreRepository,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<DesactiverMembreCommand>
{
    public async Task Handle(DesactiverMembreCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        logger.LogInformation("Command Desactiver Membre {MembreId}", request.MembreId);
        Membre membre = await membreRepository.GetByIdAsync(new MembreId(request.MembreId), cancellationToken);
        membre.Desactiver(request.Raisons);
        domainEventsContainer.AddEvents(membre.DomainEvents);
        membre.ClearEvents();
        logger.LogInformation("Membre {MembreId} desactived", membre.Id);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
