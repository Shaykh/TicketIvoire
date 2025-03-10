using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.Membres.EventHandlers;

public class MembreValideEventHandlerTests
{
    [Fact]
    public async Task GivenMembreValideEventHandler_WhenFoundEntity_ThenMembreIsValidated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenMembreValideEventHandler_WhenFoundEntity_ThenMembreIsValidated))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreValideEventHandler(Mock.Of<ILogger<MembreValideEventHandler>>(), dbContext);
        var membreId = Guid.NewGuid();
        var membreEvent = new MembreValideEvent(membreId);

        // Act
        async Task act() => await handler.HandleAsync(membreEvent, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async Task GivenMembreValideEventHandler_WhenNotFoundEntity_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenMembreValideEventHandler_WhenNotFoundEntity_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreValideEventHandler(Mock.Of<ILogger<MembreValideEventHandler>>(), dbContext);
        var membreId = Guid.NewGuid();
        var membre = new MembreEntity { Id = membreId, Login = "login", Email = "email@example.com", Nom = "Nom", Prenom = "Prenom", Telephone = "0123456789", DateAdhesion = DateTime.Now, StatutAdhesion = StatutAdhesion.EnAttente };
        await dbContext.Membres.AddAsync(membre);
        await dbContext.SaveChangesAsync();
        var membreEvent = new MembreValideEvent(membreId);

        // Act
        await handler.HandleAsync(membreEvent, CancellationToken.None);

        // Assert
        var updatedMembre = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreId);
        Assert.NotNull(updatedMembre);
        Assert.Equal(StatutAdhesion.Accepte, updatedMembre.StatutAdhesion);
    }

    [Fact]
    public void GivenIsTransactional_WhenCalled_ThenReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenIsTransactional_WhenCalled_ThenReturnTrue))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreValideEventHandler(Mock.Of<ILogger<MembreValideEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
