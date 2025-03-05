using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetPropositionEvenementByIdQuery(Guid Id) : IQuery<PropositionEvenementResponse>;

public class GetPropositionEvenementByIdQueryHandler(ILogger<GetPropositionEvenementByIdQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetPropositionEvenementByIdQuery, PropositionEvenementResponse>
{
    public async Task<PropositionEvenementResponse> Handle(GetPropositionEvenementByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get Proposition Evenement By Id: {Id}", query.Id);
        PropositionEvenement propositionEvenement = await propositionEvenementRepository.GetByIdAsync(new PropositionEvenementId(query.Id));

        PropositionEvenementResponse response = propositionEvenement.ToResponse();
        logger.LogInformation("Sortie Query Get Proposition Evenement By Id Response: {Response}", response);
        return response;
    }
}
