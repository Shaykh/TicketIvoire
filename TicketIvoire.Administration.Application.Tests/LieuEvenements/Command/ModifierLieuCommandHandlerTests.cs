using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Command;

public class ModifierLieuCommandHandlerTests
{
    private static ModifierLieuCommandHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock,
        out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new ModifierLieuCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();
        lieuRepositoryMock = new();

        return new ModifierLieuCommandHandler(Mock.Of<ILogger<ModifierLieuCommandHandler>>(), validator, domainEventsContainerMock.Object, 
            unitOfWorkMock.Object, lieuRepositoryMock.Object);
    }

    [Theory]
    [InlineData("Nom", "Description", "Adresse", "")]
    [InlineData("Nom", "Description", "", "Ville")]
    [InlineData("Nom", "", "Adresse", "Ville")]
    [InlineData("", "Description", "Adresse", "Ville")]
    public async Task GivenHandle_WhenInvalidInformation_ThenThrowException(string nom, string description, string adresse, string ville)
    {
        // Arrange
        var command = new ModifierLieuCommand(Guid.NewGuid(), 45, nom, description, adresse, ville);
        ModifierLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()), 
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenEmptyLieuId_ThenThrowException()
    {
        // Arrange
        var command = new ModifierLieuCommand(Guid.Empty, 44, "nom", "description", "adresse", "ville");
        ModifierLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenValidCommand_ThenExecute()
    {
        // Arrange
        var command = new ModifierLieuCommand(Guid.NewGuid(), 44, "nom", "description", "adresse", "ville");
        ModifierLieuCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", 455);
        lieuRepositoryMock.Setup(r => r.GetByIdAsync(command.LieuId, CancellationToken.None))
            .ReturnsAsync(lieu);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None),
            Times.Once);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(command.LieuId, CancellationToken.None),
            Times.Once);
        Assert.Empty(lieu.DomainEvents);
    }
}
