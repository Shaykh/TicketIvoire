using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Rules;

public class LieuInformationMustBeValidRule(string nom, string description, string adresse, string ville) : IBusinessRule
{
    public string Message => "Le lieu doit avoir un nom, une adresse, une description et une ville bien définie.";

    public bool Validate() => !string.IsNullOrWhiteSpace(nom) 
        && !string.IsNullOrWhiteSpace(description) 
        && !string.IsNullOrWhiteSpace(adresse) 
        && !string.IsNullOrWhiteSpace(ville);
}
