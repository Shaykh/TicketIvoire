using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.Membres.Command;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests.Membres.Command;

public class AjouterMembreCommandHandlerTests
{
    private static AjouterMembreCommandHandler MakeSut(out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new AjouterMembreCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();

        return new AjouterMembreCommandHandler(Mock.Of<ILogger<AjouterMembreCommandHandler>>(), validator, domainEventsContainerMock.Object, unitOfWorkMock.Object);
    }

    [Theory]
    [InlineData("", "email@example.com", "Nom", "Prenom", "0123456789")]
    [InlineData("login", "invalid-email", "Nom", "Prenom", "0123456789")]
    [InlineData("login", "email@example.com", "", "Prenom", "0123456789")]
    [InlineData("login", "email@example.com", "Nom", "", "0123456789")]
    [InlineData("login", "email@example.com", "Nom", "Prenom", "")]
    public async Task GivenHandle_WhenInvalidCommand_ThenThrowException(string login, string email, string nom, string prenom, string telephone)
    {
        // Arrange
        var command = new AjouterMembreCommand(login, email, nom, prenom, telephone, DateTime.UtcNow);
        AjouterMembreCommandHandler handler = MakeSut(out var domainEventsContainerMock, out var unitOfWorkMock);

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
        var command = new AjouterMembreCommand("login", "email@example.com", "Nom", "Prenom", "0123456789", DateTime.UtcNow);
        AjouterMembreCommandHandler handler = MakeSut(out var domainEventsContainerMock, out var unitOfWorkMock);

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
