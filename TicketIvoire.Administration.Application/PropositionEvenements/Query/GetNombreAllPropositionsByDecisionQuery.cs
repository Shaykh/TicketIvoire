using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetNombreAllPropositionsByDecisionQuery(PropositionDecision Decision) : IQuery<int>;

public class GetNombreAllPropositionsByDecisionQueryHandler(ILogger<GetNombreAllPropositionsByDecisionQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetNombreAllPropositionsByDecisionQuery, int>
{
    public async Task<int> Handle(GetNombreAllPropositionsByDecisionQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query GetNombre All Propositions By Decision: {Decision}", query.Decision);
        int count = await propositionEvenementRepository.GetAllCountByDecisionAsync(query.Decision);
        logger.LogInformation("Sortie Query GetNombre All Propositions By Decision {Count} propositions", count);
        return count;
    }
}
