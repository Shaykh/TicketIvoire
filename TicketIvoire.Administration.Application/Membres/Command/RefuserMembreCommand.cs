using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Membres.Command;

public record RefuserMembreCommand(Guid MembreId) : ICommand;

public class RefuserMembreCommandHandler(ILogger<RefuserMembreCommandHandler> logger,
    IMembreRepository membreRepository,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<RefuserMembreCommand>
{
    public async Task Handle(RefuserMembreCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Refuser Membre {MembreId}", command.MembreId);
        Membre membre = await membreRepository.GetByIdAsync(new MembreId(command.MembreId));
        membre.Refuser();
        domainEventsContainer.AddEvents(membre.DomainEvents);
        membre.ClearEvents();
        logger.LogInformation("Membre {MembreId} refused", membre.Id);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
