using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetNombreAllPropositionsByStatutQuery(PropositionStatut Statut) : IQuery<int>;

public class GetNombreAllPropositionsByStatutQueryHandler(ILogger<GetNombreAllPropositionsByStatutQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetNombreAllPropositionsByStatutQuery, int>
{
    public async Task<int> Handle(GetNombreAllPropositionsByStatutQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query GetNombre All Propositions By Statut: {Statut}", query.Statut);
        int count = await propositionEvenementRepository.GetAllCountByStatutAsync(query.Statut);
        logger.LogInformation("Sortie Query GetNombre All Propositions By Statut {Count} propositions", count);
        return count;
    }
}
