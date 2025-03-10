using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.LieuEvenements.EventHandlers;

public class LieuRetireEventHandlerTests
{
    [Fact]
    public async Task GivenLieuRetireEventHandler_WhenFoundEntity_ThenAddDeletionDate()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuRetireEventHandler_WhenFoundEntity_ThenAddDeletionDate))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuRetireEventHandler(Mock.Of<ILogger<LieuRetireEventHandler>>(), dbContext);
        var lieuId = Guid.NewGuid();
        var lieu = new LieuEntity { Id = lieuId, Nom = "Nom", Description = "Description", Adresse = "Adresse", Ville = "Ville" };
        await dbContext.Lieux.AddAsync(lieu);
        await dbContext.SaveChangesAsync();
        var lieuEvent = new LieuRetireEvent(lieuId, "raisons", Guid.NewGuid());

        // Act
        await handler.HandleAsync(lieuEvent, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        // Assert
        var removedLieu = await dbContext.Lieux.IgnoreQueryFilters().SingleOrDefaultAsync(l => l.Id == lieuId);
        Assert.NotNull(removedLieu);
        Assert.NotNull(removedLieu.DeletedAt);
        Assert.Equal(lieuEvent.Raisons, removedLieu.RaisonsRetrait);
    }

    [Fact]
    public async Task GivenLieuRetireEventHandler_WhenNotFoundEntity_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuRetireEventHandler_WhenNotFoundEntity_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuRetireEventHandler(Mock.Of<ILogger<LieuRetireEventHandler>>(), dbContext);
        var lieuId = Guid.NewGuid();
        var lieuEvent = new LieuRetireEvent(lieuId, "raisons", Guid.NewGuid());

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
        var handler = new LieuRetireEventHandler(Mock.Of<ILogger<LieuRetireEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
