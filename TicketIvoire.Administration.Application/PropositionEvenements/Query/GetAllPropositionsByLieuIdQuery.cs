using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsByLieuIdQuery(Guid LieuId) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsByLieuIdQueryValidator : AbstractValidator<GetAllPropositionsByLieuIdQuery>
{
    public GetAllPropositionsByLieuIdQueryValidator() => RuleFor(q => q.LieuId).NotEmpty();
}

public class GetAllPropositionsByLieuIdQueryHandler(ILogger<GetAllPropositionsByLieuIdQueryHandler> logger,
    IValidator<GetAllPropositionsByLieuIdQuery> validator,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsByLieuIdQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsByLieuIdQuery request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        logger.LogInformation("Entree Query Get All Propositions Evenement By LieuId: {LieuId}", request.LieuId);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllByLieuId(request.LieuId);
        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Propositions Evenement By LieuId: {LieuId}, Response: {Count}", request.LieuId, response.Count());
        return response;
    }
}
