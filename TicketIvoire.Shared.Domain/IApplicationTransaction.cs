namespace TicketIvoire.Shared.Domain;

public interface IApplicationTransaction
{
    Task CommitAsync(CancellationToken token);
}
