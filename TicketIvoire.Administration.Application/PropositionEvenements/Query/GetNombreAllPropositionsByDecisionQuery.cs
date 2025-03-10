using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetNombreAllPropositionsByDecisionQuery(string DecisionCode) : IQuery<int>;

public class GetNombreAllPropositionsByDecisionQueryValidator : AbstractValidator<GetNombreAllPropositionsByDecisionQuery>
{
    public GetNombreAllPropositionsByDecisionQueryValidator()
    {
        RuleFor(q => q.DecisionCode)
            .NotEmpty();
        RuleFor(q => q.DecisionCode)
            .Must(dc => dc.Equals(PropositionDecision.AccepterCode, StringComparison.OrdinalIgnoreCase)
            || dc.Equals(PropositionDecision.RefuserCode, StringComparison.OrdinalIgnoreCase));
    }
}

public class GetNombreAllPropositionsByDecisionQueryHandler(ILogger<GetNombreAllPropositionsByDecisionQueryHandler> logger,
    IValidator<GetNombreAllPropositionsByDecisionQuery> validator,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetNombreAllPropositionsByDecisionQuery, int>
{
    public async Task<int> Handle(GetNombreAllPropositionsByDecisionQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        logger.LogInformation("Entree Query GetNombre All Propositions By Decision: {Decision}", query.DecisionCode);
        int count = await propositionEvenementRepository.GetAllCountByDecisionCodeAsync(query.DecisionCode);
        logger.LogInformation("Sortie Query GetNombre All Propositions By Decision {Count} propositions", count);
        return count;
    }
}
