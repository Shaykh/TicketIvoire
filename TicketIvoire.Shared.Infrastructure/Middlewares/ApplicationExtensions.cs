using Microsoft.AspNetCore.Builder;

namespace TicketIvoire.Shared.Infrastructure.Middlewares;

public static class ApplicationExtensions
{
    public static IApplicationBuilder AddCorrelationId(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
}
