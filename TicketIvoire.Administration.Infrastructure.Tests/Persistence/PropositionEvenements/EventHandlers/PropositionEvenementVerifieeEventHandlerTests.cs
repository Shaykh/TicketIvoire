using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementVerifieeEventHandlerTests
{
    [Fact]
    public async Task GivenPropositionEvenementVerifieeEventHandler_WhenFoundEntity_ThenPropositionEvenementIsRefused()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenPropositionEvenementVerifieeEventHandler_WhenFoundEntity_ThenPropositionEvenementIsRefused))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementVerifieeEventHandler(Mock.Of<ILogger<PropositionEvenementVerifieeEventHandler>>(), dbContext);
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
        var propositionEvent = new PropositionEvenementVerifieeEvent(propositionEvenementId);

        // Act
        await handler.HandleAsync(propositionEvent, CancellationToken.None);

        // Assert
        var updatedProposition = await dbContext.PropositionEvenements.SingleOrDefaultAsync(pe => pe.Id == propositionEvenementId);
        Assert.NotNull(updatedProposition);
        Assert.Equal(PropositionStatut.Verifiee, updatedProposition.PropositionStatut);
    }

    [Fact]
    public async Task GivenPropositionEvenementVerifieeEventHandler_WhenNotFoundEntity_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenPropositionEvenementVerifieeEventHandler_WhenNotFoundEntity_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementVerifieeEventHandler(Mock.Of<ILogger<PropositionEvenementVerifieeEventHandler>>(), dbContext);
        var propositionEvenementId = Guid.NewGuid();
        var propositionEvent = new PropositionEvenementVerifieeEvent(propositionEvenementId);

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
        var handler = new PropositionEvenementVerifieeEventHandler(Mock.Of<ILogger<PropositionEvenementVerifieeEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
