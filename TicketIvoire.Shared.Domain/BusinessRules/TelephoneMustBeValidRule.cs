using System.Text.RegularExpressions;

namespace TicketIvoire.Shared.Domain.BusinessRules;

public class TelephoneMustBeValidRule(string telephone) : IBusinessRule
{
    public string Message => $"Le numéro de téléphone {telephone} ne respecte le format de numérotation de la Côte d'Ivoire";
    private const string TelephonePattern = @"^(?:\+225|00225)?[0-9]{10}$";

    public bool Validate()
    {
        if (string.IsNullOrEmpty(telephone))
        {
            return false;
        }
        string cleanedNumber = Regex.Replace(telephone, @"[\s\.-]", "");

        return Regex.IsMatch(cleanedNumber, TelephonePattern);
    }
}
