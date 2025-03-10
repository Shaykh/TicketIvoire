using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Command;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Command;

public class AjouterPropositionEvenementCommandHandlerTests
{
    private static AjouterPropositionEvenementCommandHandler MakeSut(out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new AjouterPropositionEvenementCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();

        return new AjouterPropositionEvenementCommandHandler(Mock.Of<ILogger<AjouterPropositionEvenementCommandHandler>>(), validator, domainEventsContainerMock.Object, unitOfWorkMock.Object);
    }

    [Theory]
    [InlineData("", "description", "Lieu", "Adresse", "Ville")]
    [InlineData("nom", "", "Lieu", "Adresse", "Ville")]
    public async Task GivenHandle_WhenInvalidCommand_ThenThrowException(string nom, string description, string lieuNom, string adresse, string ville)
    {
        // Arrange
        var command = new AjouterPropositionEvenementCommand(Guid.NewGuid(), nom, description, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), new PropositionEvenementLieuDto(lieuNom, adresse, ville, null));
        AjouterPropositionEvenementCommandHandler handler = MakeSut(out var domainEventsContainerMock, out var unitOfWorkMock);

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
        var command = new AjouterPropositionEvenementCommand(Guid.NewGuid(), "nom", "description", DateTime.Now, DateTime.Now.AddDays(1), 
            new PropositionEvenementLieuDto("Lieu", "Adresse", "Ville", null));
        AjouterPropositionEvenementCommandHandler handler = MakeSut(out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None),
            Times.Once);
    }
}
