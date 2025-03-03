using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Shared.Infrastructure;

public class UnitOfWork(IDbUnitOfWork dbUnitOfWork, IDomainEventsContainer domainEventsContainer, IDomainEventsDispatcher domainEventsDispatcher)
    : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await domainEventsDispatcher.DispatchAllTransactionalEventsAsync(domainEventsContainer.DomainEvents, cancellationToken);
        await dbUnitOfWork.CommitAsync(cancellationToken);
        await domainEventsDispatcher.DispatchAllNoTransactionalEventsAsync(domainEventsContainer.DomainEvents, cancellationToken);
        domainEventsContainer.ClearEvents();
    }
}
