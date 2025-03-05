using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetNombreAllPropositionsQuery() : IQuery<int>;

public class GetNombreAllPropositionsQueryHandler(ILogger<GetNombreAllPropositionsQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetNombreAllPropositionsQuery, int>
{
    public async Task<int> Handle(GetNombreAllPropositionsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query GetNombre All Propositions");
        int count = await propositionEvenementRepository.GetAllCountAsync();
        logger.LogInformation("Sortie Query GetNombre All Propositions {Count} propositions", count);
        return count;
    }
}
