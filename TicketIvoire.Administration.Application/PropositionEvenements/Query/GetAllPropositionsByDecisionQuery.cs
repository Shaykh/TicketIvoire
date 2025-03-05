using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsByDecisionQuery(PropositionDecision Decision, uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsByDecisionQueryHandler(ILogger<GetAllPropositionsByDecisionQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsByDecisionQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsByDecisionQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get All Proposition Evenement By Decision: {Decision} PageNumber: {PageNumber} NumberByPage: {NumberByPage}", request.Decision, request.PageNumber, request.NumberByPage);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllByDecisionAsync(request.Decision, request.PageNumber, request.NumberByPage);
        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Proposition Evenement By Decision: {Decision}, Response: {Count}", request.Decision, response.Count());
        return response;
    }
}
