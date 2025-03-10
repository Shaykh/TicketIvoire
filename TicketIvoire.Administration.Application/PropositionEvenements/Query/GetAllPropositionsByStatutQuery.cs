using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsByStatutQuery(PropositionStatut Statut, uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsByStatutQueryHandler(ILogger<GetAllPropositionsByStatutQueryHandler> logger,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsByStatutQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsByStatutQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get All Proposition Evenement By Statut: {Statut} PageNumber: {PageNumber} NumberByPage: {NumberByPage}", query.Statut, query.PageNumber, query.NumberByPage);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllByStatutAsync(query.Statut, query.PageNumber, query.NumberByPage, cancellationToken);
        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Proposition Evenement By Statut: {Statut}, Response: {Count}", query.Statut, response.Count());
        return response;
    }
}
