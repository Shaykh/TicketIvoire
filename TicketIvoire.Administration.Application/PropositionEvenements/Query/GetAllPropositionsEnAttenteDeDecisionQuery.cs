using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsEnAttenteDeDecisionQuery(uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsEnAttenteDeDecisionQueryHandler(ILogger<GetAllPropositionsEnAttenteDeDecisionQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsEnAttenteDeDecisionQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsEnAttenteDeDecisionQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get All Proposition Evenement en attente de decision PageNumber: {PageNumber} NumberByPage: {NumberByPage}", request.PageNumber, request.NumberByPage);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllEnAttenteDeDecisionCodeAsync(request.PageNumber, request.NumberByPage, cancellationToken);
        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Proposition Evenement en attente de decision, Response: {Count}", response.Count());
        return response;
    }
}
