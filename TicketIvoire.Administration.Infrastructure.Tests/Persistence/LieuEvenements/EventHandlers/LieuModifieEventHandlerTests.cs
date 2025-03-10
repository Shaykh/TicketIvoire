using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.LieuEvenements.EventHandlers;

public class LieuModifieEventHandlerTests
{
    [Fact]
    public async Task GivenLieuModifieEventHandler_WhenFoundEntity_ThenLieuIsUpdated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuModifieEventHandler_WhenFoundEntity_ThenLieuIsUpdated))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuModifieEventHandler(Mock.Of<ILogger<LieuModifieEventHandler>>(), dbContext);
        var lieuId = Guid.NewGuid();
        var lieu = new LieuEntity { Id = lieuId, Nom = "Nom", Description = "Description", Adresse = "Adresse", Ville = "Ville" };
        await dbContext.Lieux.AddAsync(lieu);
        await dbContext.SaveChangesAsync();
        var lieuEvent = new LieuModifieEvent(lieuId, 200, "NouveauNom", "NouvelleDescription", "NouvelleAdresse", "NouvelleVille");

        // Act
        await handler.HandleAsync(lieuEvent, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        // Assert
        var updatedLieu = await dbContext.Lieux.SingleOrDefaultAsync(l => l.Id == lieuId);
        Assert.NotNull(updatedLieu);
        Assert.Equal(lieuEvent.Nom, updatedLieu.Nom);
        Assert.Equal(lieuEvent.Description, updatedLieu.Description);
        Assert.Equal(lieuEvent.Adresse, updatedLieu.Adresse);
        Assert.Equal(lieuEvent.Ville, updatedLieu.Ville);
        Assert.Equal(lieuEvent.Capacite, updatedLieu.Capacite);
    }

    [Fact]
    public async Task GivenLieuModifieEventHandler_WhenNotFoundEntity_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuModifieEventHandler_WhenNotFoundEntity_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuModifieEventHandler(Mock.Of<ILogger<LieuModifieEventHandler>>(), dbContext);
        var lieuId = Guid.NewGuid();
        var lieuEvent = new LieuModifieEvent(lieuId, 200, "NouveauNom", "NouvelleDescription", "NouvelleAdresse", "NouvelleVille");

        // Act
        async Task act() => await handler.HandleAsync(lieuEvent, CancellationToken.None);

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
        var handler = new LieuModifieEventHandler(Mock.Of<ILogger<LieuModifieEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
