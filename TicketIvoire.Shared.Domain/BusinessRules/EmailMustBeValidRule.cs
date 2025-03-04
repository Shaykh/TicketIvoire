namespace TicketIvoire.Shared.Domain.BusinessRules;

public class EmailMustBeValidRule(string email) : IBusinessRule
{
    public string Message => $"L'email {email} n'est pas valide";

    public bool Validate()
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
