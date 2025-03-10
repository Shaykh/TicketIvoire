using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.Membres.Command;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.Membres.Command;

public class RefuserMembreCommandHandlerTests
{
    private static RefuserMembreCommandHandler MakeSut(out Mock<IMembreRepository> membreRepositoryMock, out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new RefuserMembreCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();
        membreRepositoryMock = new();

        return new RefuserMembreCommandHandler(Mock.Of<ILogger<RefuserMembreCommandHandler>>(), validator, membreRepositoryMock.Object,
            domainEventsContainerMock.Object, unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidCommand_ThenThrowException()
    {
        // Arrange
        var command = new RefuserMembreCommand(Guid.Empty);
        RefuserMembreCommandHandler handler = MakeSut(out var membreRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()), 
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
        membreRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<MembreId>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenValidCommand_ThenExecute()
    {
        // Arrange
        var command = new RefuserMembreCommand(Guid.NewGuid());
        RefuserMembreCommandHandler handler = MakeSut(out var membreRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);
        var membre = Membre.Create("login", "email@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);
        membreRepositoryMock.Setup(r => r.GetByIdAsync(new MembreId(command.MembreId), CancellationToken.None))
            .ReturnsAsync(membre);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None),
            Times.Once);
        membreRepositoryMock.Verify(r => r.GetByIdAsync(new MembreId(command.MembreId), CancellationToken.None),
            Times.Once);
        Assert.Empty(membre.DomainEvents);
    }
}
