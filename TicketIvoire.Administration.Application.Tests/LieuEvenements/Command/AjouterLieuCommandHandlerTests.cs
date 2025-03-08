using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Command;

public class AjouterLieuCommandHandlerTests
{
    private static AjouterLieuCommandHandler MakeSut(out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new AjouterLieuCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();

        return new AjouterLieuCommandHandler(Mock.Of<ILogger<AjouterLieuCommandHandler>>(), validator, domainEventsContainerMock.Object, unitOfWorkMock.Object);
    }

    [Theory]
    [InlineData("Nom", "Description", "Adresse", "")]
    [InlineData("Nom", "Description", "", "Ville")]
    [InlineData("Nom", "", "Adresse", "Ville")]
    [InlineData("", "Description", "Adresse", "Ville")]
    public async Task GivenHandle_WhenInvalidCommand_ThenThrowException(string nom, string description, string adresse, string ville)
    {
        // Arrange
        var command = new AjouterLieuCommand(null, nom, description, adresse, ville);
        AjouterLieuCommandHandler handler = MakeSut(out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()), 
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenValidCommand_ThenExecute()
    {
        // Arrange
        var command = new AjouterLieuCommand(null, "nom", "description", "adresse", "ville");
        AjouterLieuCommandHandler handler = MakeSut(out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
