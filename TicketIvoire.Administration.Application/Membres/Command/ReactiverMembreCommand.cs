using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Membres.Command;

public record ReactiverMembreCommand(Guid MembreId, string Raisons) : ICommand;

public class ReactiverMembreCommandHandler(ILogger<ReactiverMembreCommandHandler> logger,
    IMembreRepository membreRepository,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<ReactiverMembreCommand>
{
    public async Task Handle(ReactiverMembreCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Reactiver Membre {MembreId}", request.MembreId);
        Membre membre = await membreRepository.GetByIdAsync(new MembreId(request.MembreId));
        membre.Reactiver(request.Raisons);
        domainEventsContainer.AddEvents(membre.DomainEvents);
        membre.ClearEvents();
        logger.LogInformation("Membre {MembreId} reactivated", membre.Id);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
