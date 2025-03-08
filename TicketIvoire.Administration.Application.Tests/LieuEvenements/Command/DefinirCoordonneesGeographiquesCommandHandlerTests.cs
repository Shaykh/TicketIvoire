using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Command;

public class DefinirCoordonneesGeographiquesCommandHandlerTests
{
    private static DefinirCoordonneesGeographiquesCommandHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock, 
        out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new DefinirCoordonneesGeographiquesCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();
        lieuRepositoryMock = new();

        return new DefinirCoordonneesGeographiquesCommandHandler(Mock.Of<ILogger<DefinirCoordonneesGeographiquesCommandHandler>>(), validator, domainEventsContainerMock.Object, 
            unitOfWorkMock.Object, lieuRepositoryMock.Object);
    }

    [Theory]
    [InlineData(-91, 0)]
    [InlineData(91, 0)]
    [InlineData(0, -181)]
    [InlineData(0, 181)]
    public async Task GivenHandle_WhenInvalidCoordonneesGeographiques_ThenThrowException(decimal latitude, decimal longitude)
    {
        // Arrange
        var command = new DefinirCoordonneesGeographiquesCommand(Guid.NewGuid(), latitude, longitude);
        DefinirCoordonneesGeographiquesCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

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
    public async Task GivenHandle_WhenEmptyLieuId_ThenThrowException()
    {
        // Arrange
        (decimal latitude, decimal longitude) = (4.55m, -15.966m);
        var command = new DefinirCoordonneesGeographiquesCommand(Guid.Empty, latitude, longitude);
        DefinirCoordonneesGeographiquesCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

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
        var command = new DefinirCoordonneesGeographiquesCommand(Guid.NewGuid(), 45.0m, 90.0m);
        DefinirCoordonneesGeographiquesCommandHandler handler = MakeSut(out var lieuRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", 455);
        lieuRepositoryMock.Setup(r => r.GetByIdAsync(command.LieuId))
            .ReturnsAsync(lieu);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()),
            Times.Once);
        Assert.Empty(lieu.DomainEvents);
    }
}
