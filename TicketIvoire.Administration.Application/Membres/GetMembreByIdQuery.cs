using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.Membres;

public record GetMembreByIdQuery(Guid Id) : IQuery<GetMembreResponse>
{
}

public class GetMembreByIdQueryHandler(ILogger<GetMembreByIdQueryHandler> logger,
    IMembreRepository membreRepository) : IQueryHandler<GetMembreByIdQuery, GetMembreResponse>
{
    public async Task<GetMembreResponse> Handle(GetMembreByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query GetMembre By Id {Id}", query.Id);
        Membre membre = await membreRepository.GetByIdAsync(new MembreId(query.Id));

        GetMembreResponse response = membre.ToResponse();
        logger.LogInformation("Sortie Query GetMembre By Id {Response}", response);
        return response;
    }
}
