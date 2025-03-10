using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.LieuEvenements.EventHandlers;

public class LieuCoordonneesGeographiquesDefiniesEventHandlerTests
{
    [Fact]
    public async Task GivenLieuCoordonneesGeographiquesDefiniesEventHandler_WhenExistingEntity_ThenLieuIsUpdated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuCoordonneesGeographiquesDefiniesEventHandler_WhenExistingEntity_ThenLieuIsUpdated))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuCoordonneesGeographiquesDefiniesEventHandler(Mock.Of<ILogger<LieuCoordonneesGeographiquesDefiniesEventHandler>>(), dbContext);
        var lieuId = Guid.NewGuid();
        var lieu = new LieuEntity { Id = lieuId, Nom = "Nom", Description = "Description", Adresse = "Adresse", Ville = "Ville" };
        await dbContext.Lieux.AddAsync(lieu);
        await dbContext.SaveChangesAsync();
        var lieuEvent = new LieuCoordonneesGeographiquesDefiniesEvent(lieuId, 4.55m, -15.966m);

        // Act
        await handler.HandleAsync(lieuEvent, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        // Assert
        var updatedLieu = await dbContext.Lieux.SingleOrDefaultAsync(l => l.Id == lieuId);
        Assert.NotNull(updatedLieu);
        Assert.Equal(lieuEvent.Latitude, updatedLieu.CoordonneesGeographiques?.Latitude);
        Assert.Equal(lieuEvent.Longitude, updatedLieu.CoordonneesGeographiques?.Longitude);
    }

    [Fact]
    public async Task GivenLieuCoordonneesGeographiquesDefiniesEventHandler_WhenNotFoundEntity_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuCoordonneesGeographiquesDefiniesEventHandler_WhenNotFoundEntity_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuCoordonneesGeographiquesDefiniesEventHandler(Mock.Of<ILogger<LieuCoordonneesGeographiquesDefiniesEventHandler>>(), dbContext);
        var lieuId = Guid.NewGuid();
        var lieuEvent = new LieuCoordonneesGeographiquesDefiniesEvent(lieuId, 4.55m, -15.966m);

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
        var handler = new LieuCoordonneesGeographiquesDefiniesEventHandler(Mock.Of<ILogger<LieuCoordonneesGeographiquesDefiniesEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
