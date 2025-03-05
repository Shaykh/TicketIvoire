using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.LieuEvenements.Command;

public record DefinirCoordonneesGeographiquesCommand(Guid LieuId, decimal Latitude, decimal Longitude) : ICommand;

public class DefinirCoordonneesGeographiquesCommandHandler(ILogger<DefinirCoordonneesGeographiquesCommandHandler> logger,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork,
    ILieuRepository lieuRepository) : ICommandHandler<DefinirCoordonneesGeographiquesCommand>
{
    public async Task Handle(DefinirCoordonneesGeographiquesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Définition des coordonnées géographiques d'un lieu {Command}", request);
        Lieu lieu = await lieuRepository.GetByIdAsync(request.LieuId);
        lieu.DefinirCoordonneesGeographiques(new LieuCoordonneesGeographiques(request.Latitude, request.Longitude));
        domainEventsContainer.AddEvents(lieu.DomainEvents);
        lieu.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Coordonnées géographiques définies pour le lieu {LieuId}", lieu.Id.Value);
    }
}
