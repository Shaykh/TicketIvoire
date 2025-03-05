using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Membres.Command;

public record AjouterMembreCommand(string Login, string Email, string Nom, string Prenom, string Telephone, DateTime DateAdhesion) : ICommand<Guid>;

public class AjouterMembreCommandHandler(ILogger<AjouterMembreCommandHandler> logger,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<AjouterMembreCommand, Guid>
{
    public async Task<Guid> Handle(AjouterMembreCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Add Membre {Command}", command);
        var membre = Membre.Create(command.Login, command.Email, command.Nom, command.Prenom, command.Telephone, command.DateAdhesion);
        domainEventsContainer.AddEvents(membre.DomainEvents);
        membre.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Membre {MembreId} created", membre.Id);
        return membre.Id.Value;
    }
}
