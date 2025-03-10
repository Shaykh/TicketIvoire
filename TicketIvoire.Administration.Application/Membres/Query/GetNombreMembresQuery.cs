using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.Membres.Query;

public record GetNombreMembresQuery(StatutAdhesion? StatutAdhesion) : IQuery<int>;

public class GetNombreMembresQueryHandler(ILogger<GetNombreMembresQueryHandler> logger,
    IMembreRepository membreRepository) : IQueryHandler<GetNombreMembresQuery, int>
{
    public async Task<int> Handle(GetNombreMembresQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query GetNombre Membres statut adhesion: {StatutAdhesion}", query.StatutAdhesion);
        int count = query.StatutAdhesion is null ?
            await membreRepository.GetAllCountAsync(cancellationToken) :
            await membreRepository.GetCountByStatutAdhesionAsync(query.StatutAdhesion.Value, cancellationToken);
        logger.LogInformation("Sortie Query GetNombre Membres {Count} membres statut adhesion: {StatutAdhesion}", count, query.StatutAdhesion);
        return count;
    }
}
