using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.LieuEvenements;

public class LieuRepositoryTests
{
    [Fact]
    public async Task GivenGetAllAsync_WhenNumberByPageNullAndPageNumberNull_ThenReturnAllData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllAsync_WhenNumberByPageNullAndPageNumberNull_ThenReturnAllData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new LieuRepository(dbContext);
        List<LieuEntity> entities = [
            new() { Adresse = "Adresse1", Description = "Description1", Nom = "Nom1", Ville = "Ville1"},
            new() { Adresse = "Adresse2", Description = "Description2", Nom = "Nom2", Ville = "Ville2"},
            new() { Adresse = "Adresse3", Description = "Description3", Nom = "Nom3", Ville = "Ville3"},
            new() { Adresse = "Adresse4", Description = "Description4", Nom = "Nom4", Ville = "Ville4"}
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllAsync(null, null, CancellationToken.None);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public async Task GivenGetAllAsync_WhenNumberByPageNotNullAndPageNumberNotNull_ThenReturnNumberByPageData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllAsync_WhenNumberByPageNotNullAndPageNumberNotNull_ThenReturnNumberByPageData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new LieuRepository(dbContext);
        List<LieuEntity> entities = [
            new() { Adresse = "Adresse1", Description = "Description1", Nom = "Nom1", Ville = "Ville1"},
            new() { Adresse = "Adresse2", Description = "Description2", Nom = "Nom2", Ville = "Ville2"},
            new() { Adresse = "Adresse3", Description = "Description3", Nom = "Nom3", Ville = "Ville3"},
            new() { Adresse = "Adresse4", Description = "Description4", Nom = "Nom4", Ville = "Ville4"}
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        uint numberByPage = 3;
        uint pageNumber = 1;

        //Act
        var result = await sut.GetAllAsync(pageNumber, numberByPage, CancellationToken.None);

        // Assert
        Assert.Equal((int)numberByPage, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByCapaciteRangeAsync_WhenMinimuNullAndMaximumNull_ThenReturnAllData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByCapaciteRangeAsync_WhenMinimuNullAndMaximumNull_ThenReturnAllData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new LieuRepository(dbContext);
        List<LieuEntity> entities = [
            new() { Adresse = "Adresse1", Description = "Description1", Nom = "Nom1", Ville = "Ville1", Capacite = 100},
            new() { Adresse = "Adresse2", Description = "Description2", Nom = "Nom2", Ville = "Ville2", Capacite = 150},
            new() { Adresse = "Adresse3", Description = "Description3", Nom = "Nom3", Ville = "Ville3", Capacite = 135},
            new() { Adresse = "Adresse4", Description = "Description4", Nom = "Nom4", Ville = "Ville4", Capacite = 160}
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        uint? minimum = null;
        uint? maximum = null;

        //Act
        var result = await sut.GetAllByCapaciteRangeAsync(minimum, maximum, CancellationToken.None);

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByCapaciteRangeAsync_WhenMinimuNullAndMaximumNotNull_ThenReturnAllDataLessOrEqualMaximum()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByCapaciteRangeAsync_WhenMinimuNullAndMaximumNotNull_ThenReturnAllDataLessOrEqualMaximum))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new LieuRepository(dbContext);
        List<LieuEntity> entities = [
            new() { Adresse = "Adresse1", Description = "Description1", Nom = "Nom1", Ville = "Ville1", Capacite = 100},
            new() { Adresse = "Adresse2", Description = "Description2", Nom = "Nom2", Ville = "Ville2", Capacite = 150},
            new() { Adresse = "Adresse3", Description = "Description3", Nom = "Nom3", Ville = "Ville3", Capacite = 135},
            new() { Adresse = "Adresse4", Description = "Description4", Nom = "Nom4", Ville = "Ville4", Capacite = 160}
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        uint? minimum = null;
        uint? maximum = 150;

        //Act
        var result = await sut.GetAllByCapaciteRangeAsync(minimum, maximum, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByCapaciteRangeAsync_WhenMinimuNotNullAndMaximumNull_ThenReturnAllDataGreaterOrEqualMaximum()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByCapaciteRangeAsync_WhenMinimuNotNullAndMaximumNull_ThenReturnAllDataGreaterOrEqualMaximum))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new LieuRepository(dbContext);
        List<LieuEntity> entities = [
            new() { Adresse = "Adresse1", Description = "Description1", Nom = "Nom1", Ville = "Ville1", Capacite = 100},
            new() { Adresse = "Adresse2", Description = "Description2", Nom = "Nom2", Ville = "Ville2", Capacite = 150},
            new() { Adresse = "Adresse3", Description = "Description3", Nom = "Nom3", Ville = "Ville3", Capacite = 135},
            new() { Adresse = "Adresse4", Description = "Description4", Nom = "Nom4", Ville = "Ville4", Capacite = 160}
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        uint? minimum = 150;
        uint? maximum = null;

        //Act
        var result = await sut.GetAllByCapaciteRangeAsync(minimum, maximum, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByCapaciteRangeAsync_WhenMinimuNotNullAndMaximumNotNull_ThenReturnAllDataInRange()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByCapaciteRangeAsync_WhenMinimuNotNullAndMaximumNotNull_ThenReturnAllDataInRange))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new LieuRepository(dbContext);
        List<LieuEntity> entities = [
            new() { Adresse = "Adresse1", Description = "Description1", Nom = "Nom1", Ville = "Ville1", Capacite = 100},
            new() { Adresse = "Adresse2", Description = "Description2", Nom = "Nom2", Ville = "Ville2", Capacite = 150},
            new() { Adresse = "Adresse3", Description = "Description3", Nom = "Nom3", Ville = "Ville3", Capacite = 135},
            new() { Adresse = "Adresse4", Description = "Description4", Nom = "Nom4", Ville = "Ville4", Capacite = 160}
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        int minimum = 145;
        int maximum = 155;

        //Act
        var result = await sut.GetAllByCapaciteRangeAsync((uint)minimum, (uint)maximum, CancellationToken.None);

        // Assert
        var actual = Assert.Single(result);
        Assert.InRange((int)actual.Capacite!.Value, minimum, maximum);
    }
}
