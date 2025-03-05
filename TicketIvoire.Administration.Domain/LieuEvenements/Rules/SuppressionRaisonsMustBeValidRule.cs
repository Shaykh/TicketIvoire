using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Rules;
public class SuppressionRaisonsMustBeValidRule(string raisons) : IBusinessRule
{
    public string Message => $"Les raisons de suppression {raisons} ne sont pas valides";

    public bool Validate() => !string.IsNullOrWhiteSpace(raisons) && raisons.Trim().Length > 3;
}
