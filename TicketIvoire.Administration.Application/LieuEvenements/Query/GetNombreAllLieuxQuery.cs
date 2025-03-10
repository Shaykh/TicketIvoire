using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetNombreAllLieuxQuery() : IQuery<int>;

public class GetNombreAllLieuxQueryHandler(ILogger<GetNombreAllLieuxQueryHandler> logger,
    ILieuRepository lieuRepository) : IQueryHandler<GetNombreAllLieuxQuery, int>
{
    public async Task<int> Handle(GetNombreAllLieuxQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get Nombre All Lieux");
        int nombre = await lieuRepository.GetCountAsync(cancellationToken);
        logger.LogInformation("Sortie Query Get Nombre All Lieux : {Count}", nombre);
        return nombre;
    }
}
