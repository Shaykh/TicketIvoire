using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Rules;

public class CoordonneesGeographiquesMustBeValidRule(decimal latitude, decimal longitude) : IBusinessRule
{
    public string Message => $"Les coordonnées géographiques {latitude}, {longitude} ne sont pas valides";

    public bool Validate() => latitude >= -90 && latitude <= 90 && longitude >= -180 && longitude <= 180;
}
