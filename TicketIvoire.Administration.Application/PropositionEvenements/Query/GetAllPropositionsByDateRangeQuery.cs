using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsByDateRangeQuery(DateTime DateDebut, DateTime DateFin) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsByDateRangeQueryValidator : AbstractValidator<GetAllPropositionsByDateRangeQuery>
{
    public GetAllPropositionsByDateRangeQueryValidator() => RuleFor(q => q).Must(q => q.DateFin > q.DateDebut);
}

public class GetAllPropositionsByDateRangeQueryHandler(ILogger<GetAllPropositionsByDateRangeQueryHandler> logger,
    IValidator<GetAllPropositionsByDateRangeQuery> validator,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsByDateRangeQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsByDateRangeQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        logger.LogInformation("Entree Query Get All Propositions Evenement from: {DateDebut} to: {DateFin}", query.DateDebut, query.DateFin);
        IEnumerable<PropositionEvenement> propositions = await propositionEvenementRepository.GetAllByDateRangeAsync(query.DateDebut, query.DateFin);
        IEnumerable<PropositionEvenementResponse> propositionsResponse = propositions.Select(p => p.ToResponse());
        logger.LogInformation("Sortie Query Get All Propositions Evenement By DateRange {Count} propositions", propositionsResponse.Count());
        return propositionsResponse;
    }
}
