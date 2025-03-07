using Moq;
using TicketIvoire.Shared.Domain.Events;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Shared.Infrastructure.Tests;

public class UnitOfWorkTests
{
    [Fact]
    public async Task GivenUnitOfWork_WhenCommitAsync_ThenDispatchEvents()
    {
        // Arrange
        var dbUnitOfWorkMock = new Mock<IDbUnitOfWork>();
        var domainEventsContainerMock = new Mock<IDomainEventsContainer>();
        var domainEventsDispatcherMock = new Mock<IDomainEventsDispatcher>();
        var sut = new UnitOfWork(dbUnitOfWorkMock.Object, domainEventsContainerMock.Object, domainEventsDispatcherMock.Object);

        // Act
        await sut.CommitAsync(CancellationToken.None);

        // Assert
        domainEventsDispatcherMock.Verify(d => d.DispatchAllTransactionalEventsAsync(domainEventsContainerMock.Object.DomainEvents, CancellationToken.None), Times.Once);
        dbUnitOfWorkMock.Verify(d => d.CommitAsync(CancellationToken.None), Times.Once);
        domainEventsDispatcherMock.Verify(d => d.DispatchAllNoTransactionalEventsAsync(domainEventsContainerMock.Object.DomainEvents, CancellationToken.None), Times.Once);
        domainEventsContainerMock.Verify(d => d.ClearEvents(), Times.Once);
    }
}
