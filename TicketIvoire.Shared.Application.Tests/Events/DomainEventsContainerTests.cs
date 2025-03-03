using Moq;
using TicketIvoire.Shared.Application.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Application.Tests.Events;

#pragma warning disable CA1707
public class DomainEventsContainerTests
{
    [Fact]
    public void GivenRegisterEvent_WhenEvent_ThenAddEventToDomainEvents()
    {
        // Arrange
        var sut = new DomainEventsContainer();
        var domainEventMock = new Mock<IDomainEvent>();

        // Act
        sut.RegisterEvent(domainEventMock.Object);

        // Assert
        Assert.Single(sut.DomainEvents);
    }

    [Fact]
    public void GivenRegisterEvent_WhenNullEvent_ThenThrowException()
    {
        // Arrange
        var sut = new DomainEventsContainer();

        // Act
        void act() => sut.RegisterEvent(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
        Assert.Empty(sut.DomainEvents);
    }

    [Fact]
    public void GivenClearEvents_WhenEvents_ThenClearDomainEvents()
    {
        // Arrange
        var sut = new DomainEventsContainer();
        var domainEventMock = new Mock<IDomainEvent>();
        sut.RegisterEvent(domainEventMock.Object);

        // Act
        sut.ClearEvents();

        // Assert
        Assert.Empty(sut.DomainEvents);
    }
}
#pragma warning restore CA1707
