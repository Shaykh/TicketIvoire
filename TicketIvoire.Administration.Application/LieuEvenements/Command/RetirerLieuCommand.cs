﻿using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Application.Commands;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.LieuEvenements.Command;

public record RetirerLieuCommand(Guid LieuId, Guid UtilisateurId, string Raisons) : ICommand;

public class RetirerLieuCommandHandler(ILogger<RetirerLieuCommandHandler> logger,
    IDomainEventsContainer domainEventsContainer,
    IUnitOfWork unitOfWork,
    ILieuRepository lieuRepository) : ICommandHandler<RetirerLieuCommand>
{
    public async Task Handle(RetirerLieuCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Command Retrait d'un lieu {LieuId} pour raisons: {Raisons} par {UtilisateurId}", request.LieuId, request.Raisons, request.UtilisateurId);
        Lieu lieu = await lieuRepository.GetByIdAsync(request.LieuId);
        lieu.Supprimer(request.Raisons, new UtilisateurId(request.UtilisateurId));
        domainEventsContainer.AddEvents(lieu.DomainEvents);
        lieu.ClearEvents();
        await unitOfWork.CommitAsync(cancellationToken);
        logger.LogInformation("Lieu retiré {LieuId}", lieu.Id.Value);
    }
}
