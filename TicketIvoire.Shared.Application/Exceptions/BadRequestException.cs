using FluentValidation.Results;
using System.Text;

namespace TicketIvoire.Shared.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }

    public BadRequestException(ValidationResult validationResult) : base(GetDetails(validationResult))
    {
    }

    public BadRequestException()
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    private static string GetDetails(ValidationResult validationResult)
    {
        var stringBuilder = new StringBuilder("Erreur de validation de la requête : ");
        foreach (ValidationFailure item in validationResult.Errors)
        {
            stringBuilder.AppendLine(item.ErrorMessage);
        }

        return stringBuilder.ToString();
    }
}
