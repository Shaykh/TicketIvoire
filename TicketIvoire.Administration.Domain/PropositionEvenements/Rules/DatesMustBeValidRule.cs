using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Rules;
public class DatesMustBeValidRule(DateTime DateDebut, DateTime DateFin) : IBusinessRule
{
    public string Message => "La date de début doit être antérieure à la date de fin.";

    public bool Validate() => DateTime.Compare(DateDebut, DateFin) < 0;
}
