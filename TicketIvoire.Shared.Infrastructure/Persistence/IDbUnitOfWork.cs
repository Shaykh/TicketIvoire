namespace TicketIvoire.Shared.Infrastructure.Persistence;

public interface IDbUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}
