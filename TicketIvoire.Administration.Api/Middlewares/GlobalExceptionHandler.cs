using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Administration.Api.Middlewares;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Erreur serveur {Message}", exception.Message);

        ProblemDetails details = SetProblemDetails(exception, out int statusCode);

        httpContext
            .Response
                .StatusCode = statusCode;

        await httpContext
                .Response
                    .WriteAsJsonAsync(details, cancellationToken);

        return true;
    }

    private static ProblemDetails SetProblemDetails(Exception exception, out int statusCode)
    {
        if (exception is BrokenBusinessRuleException brokenBusinessRuleException)
        {
            statusCode = StatusCodes.Status403Forbidden;
            return new ProblemDetails
            {
                Status = statusCode,
                Detail = brokenBusinessRuleException.Message,
                Title = "Non respect de règle de gestion"
            };
        }

        if (exception is NotFoundException notFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            return new ProblemDetails
            {
                Status = statusCode,
                Detail = notFoundException.Message,
                Title = "Entité non trouvée"
            };
        }

        if (exception is DataAccessException dataNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            return new ProblemDetails
            {
                Status = statusCode,
                Detail = dataNotFoundException.Message,
                Title = "Entité non trouvée"
            };
        }

        if (exception is BadRequestException badRequestException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            return new ProblemDetails
            {
                Status = statusCode,
                Detail = badRequestException.Message,
                Title = "Requête non valide"
            };
        }

        statusCode = StatusCodes.Status500InternalServerError;
        return new ProblemDetails
        {
            Status = statusCode,
            Detail = "Une erreur est apparue lors du traitement de votre requête",
            Title = "Erreur Serveur",
        };
    }
}
