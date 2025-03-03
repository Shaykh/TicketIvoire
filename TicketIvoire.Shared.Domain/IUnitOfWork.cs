namespace TicketIvoire.Shared.Domain;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}
