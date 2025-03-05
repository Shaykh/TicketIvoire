using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.Membres.Query;

public record GetAllMembresQuery(StatutAdhesion? StatutAdhesion, uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<GetMembreResponse>>;

public class GetAllQueryHandler(ILogger<GetAllQueryHandler> logger,
    IMembreRepository membreRepository) : IQueryHandler<GetAllMembresQuery, IEnumerable<GetMembreResponse>>
{
    public async Task<IEnumerable<GetMembreResponse>> Handle(GetAllMembresQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query GetAll Membres page: {PageNumber}, number by page: {NumberByPage}, statut adhesion: {StatutAdhesion}", query.PageNumber, query.NumberByPage, query.StatutAdhesion);
        IEnumerable<Membre> membres = query.StatutAdhesion.HasValue ? 
            await membreRepository.GetAllByStatutAdhesionAsync(query.StatutAdhesion.Value, query.PageNumber, query.NumberByPage) :
            await membreRepository.GetAllAsync(query.PageNumber, query.NumberByPage);

        IEnumerable<GetMembreResponse> response = membres.Select(m => m.ToResponse());
        logger.LogInformation("Sortie Query GetAll {Count} Membres, statut adhesion: {StatutAdhesion}", response.Count(), query.StatutAdhesion);
        return response;
    }
}
