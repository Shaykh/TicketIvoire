namespace TicketIvoire.Shared.Domain.BusinessRules;
public class IdMustBeValidRule(Guid value) : IBusinessRule
{
    public string Message => $"L'identifiant {value} n'est pas valide";

    public bool Validate() => value != Guid.Empty;
}
