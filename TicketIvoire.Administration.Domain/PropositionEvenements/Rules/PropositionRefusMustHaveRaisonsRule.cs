using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Rules;

public class PropositionRefusMustHaveRaisonsRule(string raisons) : IBusinessRule
{
    public string Message => "Les raisons du refus doivent être renseignées.";

    public bool Validate() => !string.IsNullOrWhiteSpace(raisons);
}
