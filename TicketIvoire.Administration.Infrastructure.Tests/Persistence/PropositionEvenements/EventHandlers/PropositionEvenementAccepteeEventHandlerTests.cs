using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementAccepteeEventHandlerTests
{
    [Fact]
    public async Task GivenPropositionEvenementAccepteeEventHandler_WhenFoundEntity_ThenPropositionEvenementIsAccepted()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenPropositionEvenementAccepteeEventHandler_WhenFoundEntity_ThenPropositionEvenementIsAccepted))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementAccepteeEventHandler(Mock.Of<ILogger<PropositionEvenementAccepteeEventHandler>>(), dbContext);
        var propositionEvenementId = Guid.NewGuid();
        var propositionEvenement = new PropositionEvenementEntity
        {
            Id = propositionEvenementId,
            Nom = "Nom1",
            Description = "Description1",
            UtilisateurId = Guid.NewGuid(),
            DateDebut = DateTime.Today,
            DateFin = DateTime.Today.AddDays(1),
            Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null)
        };
        await dbContext.PropositionEvenements.AddAsync(propositionEvenement);
        await dbContext.SaveChangesAsync();
        var propositionEvent = new PropositionEvenementAccepteeEvent(propositionEvenementId, Guid.NewGuid(), DateTime.UtcNow);

        // Act
        await handler.HandleAsync(propositionEvent, CancellationToken.None);

        // Assert
        var updatedProposition = await dbContext.PropositionEvenements.SingleOrDefaultAsync(pe => pe.Id == propositionEvenementId);
        Assert.NotNull(updatedProposition);
        Assert.NotNull(updatedProposition.Decision);
        Assert.Equal(PropositionDecision.AccepterCode, updatedProposition.Decision?.Code);
        Assert.Equal(propositionEvent.UtilisateurId, updatedProposition.Decision?.UtilisateurId);
        Assert.Equal(propositionEvent.DateDecision, updatedProposition.Decision?.DateDecision);
        Assert.Null(updatedProposition.Decision?.Raisons);
    }

    [Fact]
    public async Task GivenPropositionEvenementAccepteeEventHandler_WhenNotFoundEntity_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenPropositionEvenementAccepteeEventHandler_WhenNotFoundEntity_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementAccepteeEventHandler(Mock.Of<ILogger<PropositionEvenementAccepteeEventHandler>>(), dbContext);
        var propositionEvenementId = Guid.NewGuid();
        var propositionEvent = new PropositionEvenementAccepteeEvent(propositionEvenementId, Guid.NewGuid(), DateTime.UtcNow);

        // Act
        async Task act() => await handler.HandleAsync(propositionEvent, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public void GivenIsTransactional_WhenCalled_ThenReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenIsTransactional_WhenCalled_ThenReturnTrue))
            .Options;
        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementAccepteeEventHandler(Mock.Of<ILogger<PropositionEvenementAccepteeEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
