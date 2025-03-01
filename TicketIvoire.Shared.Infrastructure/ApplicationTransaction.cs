using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Shared.Infrastructure;

public class ApplicationTransaction() : IApplicationTransaction
{
    public Task CommitAsync(CancellationToken token) => throw new NotImplementedException();
}
