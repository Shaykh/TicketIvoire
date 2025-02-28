namespace TicketIvoire.Shared.Domain.BusinessRules;

public interface IBusinessRule
{
    bool Validate();
    string Message { get; }
}
