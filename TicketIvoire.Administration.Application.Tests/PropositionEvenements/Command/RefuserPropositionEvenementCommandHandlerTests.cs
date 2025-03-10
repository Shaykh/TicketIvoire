using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Command;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;
using Xunit;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Command;

public class RefuserPropositionEvenementCommandHandlerTests
{
    private static RefuserPropositionEvenementCommandHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionRepositoryMock, 
        out Mock<IDomainEventsContainer> domainEventsContainerMock, out Mock<IUnitOfWork> unitOfWorkMock)
    {
        var validator = new RefuserPropositionEvenementCommandValidator();
        domainEventsContainerMock = new();
        unitOfWorkMock = new();
        propositionRepositoryMock = new();

        return new RefuserPropositionEvenementCommandHandler(Mock.Of<ILogger<RefuserPropositionEvenementCommandHandler>>(), validator, 
            propositionRepositoryMock.Object, domainEventsContainerMock.Object, unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidCommand_ThenThrowException()
    {
        // Arrange
        var command = new RefuserPropositionEvenementCommand(Guid.Empty, Guid.Empty, DateTime.Now, "");
        RefuserPropositionEvenementCommandHandler handler = MakeSut(out var propositionRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);

        // Act
        async Task act() => await handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()), 
            Times.Never);
        unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
        propositionRepositoryMock.Verify(p => p.GetByIdAsync(It.IsAny<PropositionEvenementId>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenValidCommand_ThenExecute()
    {
        // Arrange
        var command = new RefuserPropositionEvenementCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "raisons");
        RefuserPropositionEvenementCommandHandler handler = MakeSut(out var propositionRepositoryMock, out var domainEventsContainerMock, out var unitOfWorkMock);
        var proposition = PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom", "description", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu1", "Adresse1", "Ville1", null));
        propositionRepositoryMock.Setup(r => r.GetByIdAsync(new PropositionEvenementId(command.PropositionEvenementId), CancellationToken.None))
            .ReturnsAsync(proposition);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        domainEventsContainerMock.Verify(d => d.AddEvents(It.IsAny<IEnumerable<IDomainEvent>>()),
            Times.Once);
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None),
            Times.Once);
        propositionRepositoryMock.Verify(r => r.GetByIdAsync(new PropositionEvenementId(command.PropositionEvenementId), CancellationToken.None),
            Times.Once);
    }
}
