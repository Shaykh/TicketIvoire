using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.Membres.Rules;
public class LoginMustBeValidRule(string login) : IBusinessRule
{
    public string Message => $"Le login doit avoir entre 3 et 50 caractères. {login} n'est donc pas valide";

    public bool Validate()
        => !string.IsNullOrEmpty(login) && login.Length >= 3 && login.Length <= 50;
}
