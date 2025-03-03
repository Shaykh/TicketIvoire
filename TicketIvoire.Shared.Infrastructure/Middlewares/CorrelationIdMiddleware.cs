using Microsoft.AspNetCore.Http;

namespace TicketIvoire.Shared.Infrastructure.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    public async Task Invoke(HttpContext context)
    {
        AddCorrelationIdToResponse(context);

        await next(context);
    }

    private static string GetCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out Microsoft.Extensions.Primitives.StringValues correlationId))
        {
            return correlationId.FirstOrDefault() ?? SetNewCorrelationId();
        }
        return SetNewCorrelationId();
    }

    private static string SetNewCorrelationId() => Guid.NewGuid().ToString();

    private static void AddCorrelationIdToResponse(HttpContext context)
    {
        string correlationId = GetCorrelationId(context);
        context.TraceIdentifier = correlationId;
        context
            .Response
                .OnStarting(() =>
                {
                    context.Response.Headers.Append(CorrelationIdHeaderName, new[] { correlationId });
                    return Task.CompletedTask;
                });
    }
}
