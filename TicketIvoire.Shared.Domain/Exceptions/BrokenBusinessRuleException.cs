using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Shared.Domain.Exceptions;

public class BrokenBusinessRuleException : Exception
{
    public required IBusinessRule BrokenRule { get; init; }

    public override string ToString()
        => FullMessage();

    public string FullMessage()
        => $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";

    public BrokenBusinessRuleException()
    {
    }

    public BrokenBusinessRuleException(IBusinessRule brokenRule) => BrokenRule = brokenRule;

    public BrokenBusinessRuleException(string message) : base(message)
    {
    }

    public BrokenBusinessRuleException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
