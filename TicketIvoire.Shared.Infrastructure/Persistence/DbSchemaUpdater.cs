using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TicketIvoire.Shared.Infrastructure.Persistence;

public static class DbSchemaUpdater
{
    public static async Task ApplyMigrationAsync<TContext>(this IServiceCollection services, CancellationToken cancellationToken) where TContext : DbContext
    {
        TContext context = services.BuildServiceProvider().GetRequiredService<TContext>();
        await context.Database.MigrateAsync(cancellationToken);
    }
}
