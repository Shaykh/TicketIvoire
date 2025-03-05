using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.LieuEvenements.Command;

public record AjouterLieuCommand(uint? Capacite, string Nom, string Description, string Adresse, string Ville) : ICommand<Guid>;

public class AjouterLieuCommandHandler(ILogger<AjouterLieuCommandHandler> logger,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork) : ICommandHandler<AjouterLieuCommand, Guid>
{
    public async Task<Guid> Handle(AjouterLieuCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Ajout d'un lieu {Command}", request);
        var lieu = Lieu.Create(request.Nom, request.Description, request.Adresse, request.Ville, request.Capacite);
        domainEventsContainer.AddEvents(lieu.DomainEvents);
        lieu.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Lieu ajouté {LieuId}", lieu.Id.Value);
        return lieu.Id.Value;
    }
}
