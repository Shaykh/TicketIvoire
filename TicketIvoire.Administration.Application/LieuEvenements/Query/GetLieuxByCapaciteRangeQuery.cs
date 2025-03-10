using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetLieuxByCapaciteRangeQuery(uint Minimum, uint Maximum) : IQuery<IEnumerable<GetLieuResponse>>;

public class GetLieuxByCapaciteRangeQueryValidator : AbstractValidator<GetLieuxByCapaciteRangeQuery>
{
    public GetLieuxByCapaciteRangeQueryValidator()
    {
        RuleFor(q => q)
            .Must(q => q.Maximum > q.Minimum);
        RuleFor(q => q.Minimum)
            .Must(m => m >= 0);
    }
}

public class GetLieuxByCapaciteRangeQueryHandler(ILogger<GetLieuxByCapaciteRangeQueryHandler> logger,
    IValidator<GetLieuxByCapaciteRangeQuery> validator,
    ILieuRepository lieuRepository) : IQueryHandler<GetLieuxByCapaciteRangeQuery, IEnumerable<GetLieuResponse>>
{
    public async Task<IEnumerable<GetLieuResponse>> Handle(GetLieuxByCapaciteRangeQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        logger.LogInformation("Entree Query Get Lieux By Capacite Range: {Minimum} - {Maximum}", query.Minimum, query.Maximum);
        IEnumerable<Lieu> lieux = await lieuRepository.GetAllByCapaciteRangeAsync(query.Minimum, query.Maximum, cancellationToken);
        IEnumerable<GetLieuResponse> response = lieux.Select(lieu => lieu.ToGetLieuResponse());
        logger.LogInformation("Sortie Query Get Lieux By Capacite from: {Minimum} to {Maximum}, Nombre: {Count}", query.Minimum, query.Maximum, response.Count());
        return response;
    }
}
