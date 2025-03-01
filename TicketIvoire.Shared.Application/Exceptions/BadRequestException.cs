using FluentValidation.Results;
using System.Text;

namespace TicketIvoire.Shared.Application.Exceptions;

public class BadRequestException : Exception
{
    /// <summary>
    /// Details de l'exception
    /// </summary>
    public required string Details { get; set; }

    public BadRequestException(string details) => Details = details;

    public BadRequestException(ValidationResult validationResult)
    {
        var stringBuilder = new StringBuilder();
        foreach (ValidationFailure item in validationResult.Errors)
        {
            stringBuilder.AppendLine(item.ErrorMessage);
        }

        Details = stringBuilder.ToString();
    }
    public BadRequestException()
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
