using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Command;

public class RetirerLieuCommandHandlerTests
{
    private static RetirerLieuCommandHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock, 
        out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new RetirerLieuCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();
        lieuRepositoryMock = new();

        return new RetirerLieuCommandHandler(Mock.Of<ILogger<RetirerLieuCommandHandler>>(), validator, domainEventsContainerMock.Object, 
            unitOfWorkMock.Object, lieuRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidLieuId_ThenThrowException()
    {
        // Arrange
        var command = new RetirerLieuCommand(Guid.Empty, Guid.NewGuid(), "raisons");
        RetirerLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()), 
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidUtilisateurId_ThenThrowException()
    {
        // Arrange
        var command = new RetirerLieuCommand(Guid.NewGuid(), Guid.Empty, "raisons");
        RetirerLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidRaisons_ThenThrowException()
    {
        // Arrange
        var command = new RetirerLieuCommand(Guid.NewGuid(), Guid.NewGuid(), "");
        RetirerLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenValidCommand_ThenExecute()
    {
        // Arrange
        var command = new RetirerLieuCommand(Guid.NewGuid(), Guid.NewGuid(), "raisons");
        RetirerLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", 455);
        lieuRepositoryMock.Setup(r => r.GetByIdAsync(command.LieuId))
            .ReturnsAsync(lieu);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None),
            Times.Once);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(command.LieuId),
            Times.Once);
        Assert.Empty(lieu.DomainEvents);
    }
}
