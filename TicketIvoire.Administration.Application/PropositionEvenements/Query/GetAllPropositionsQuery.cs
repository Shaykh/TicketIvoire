using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsQuery(uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsQueryHandler(ILogger<GetAllPropositionsQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get All Proposition Evenement PageNumber: {PageNumber} NumberByPage: {NumberByPage}", query.PageNumber, query.NumberByPage);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllAsync(query.PageNumber, query.NumberByPage);

        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Proposition Evenement Response: {Count}", response.Count());
        return response;
    }
}
