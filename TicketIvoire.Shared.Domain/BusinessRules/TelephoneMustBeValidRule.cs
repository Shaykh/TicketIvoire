using System.Text.RegularExpressions;

namespace TicketIvoire.Shared.Domain.BusinessRules;

public class TelephoneMustBeValidRule(string telephone) : IBusinessRule
{
    public string Message => $"Le numéro de téléphone {telephone} ne respecte le format de numérotation de la Côte d'Ivoire";
    private const string TelephonePattern = @"^(0(1|5|7|21|22|23|24|25|26|27)\d{7}|\+225(1|5|7|21|22|23|24|25|26|27)\d{7})$";

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
