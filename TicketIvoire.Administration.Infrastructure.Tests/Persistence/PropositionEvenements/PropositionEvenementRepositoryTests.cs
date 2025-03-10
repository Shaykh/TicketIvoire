using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.PropositionEvenements;

public class PropositionEvenementRepositoryTests
{
    [Fact]
    public async Task GivenGetAllAsync_WhenNumberByPageNullAndPageNumberNull_ThenReturnAllData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllAsync_WhenNumberByPageNullAndPageNumberNull_ThenReturnAllData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today, 
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        uint? pageNumber = null;
        uint? numberByPage = null;

        //Act
        var result = await sut.GetAllAsync(pageNumber, numberByPage);

        //Assert
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
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        uint? pageNumber = 1;
        uint? numberByPage = 3;

        //Act
        var result = await sut.GetAllAsync(pageNumber, numberByPage);

        //Assert
        Assert.Equal((int)numberByPage.Value, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByDateRangeAsync_WhenExistingEntities_ThenReturnCorrectData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByDateRangeAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        var dateDebut = DateTime.Today.AddDays(3);
        var datefin = DateTime.Today.AddDays(5);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = dateDebut,
                DateFin = datefin, Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = dateDebut,
                DateFin = datefin, Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllByDateRangeAsync(dateDebut, datefin);

        //Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByDateRangeAsync_WhenNoExistingEntities_ThenReturnEmpty()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByDateRangeAsync_WhenNoExistingEntities_ThenReturnEmpty))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        var dateDebut = DateTime.Today.AddDays(3);
        var datefin = DateTime.Today.AddDays(5);

        //Act
        var result = await sut.GetAllByDateRangeAsync(dateDebut, datefin);

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GivenGetAllByDecisionCodeAsync_WhenExistingEntities_ThenReturnCorrectData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByDecisionCodeAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null")},
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null") }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllByDecisionCodeAsync(PropositionDecision.AccepterCode, null, null);

        //Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GivenGetAllEnAttenteDeDecisionCodeAsync_WhenExistingEntities_ThenReturnCorrectData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllEnAttenteDeDecisionCodeAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null")},
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null") }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllEnAttenteDeDecisionCodeAsync(null, null);

        //Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GivenGetAllByLieuIdAsync_WhenExistingEntities_ThenReturnCorrectData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByLieuIdAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        var lieuId = Guid.NewGuid();
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu(null, null, null, lieuId) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu(null, null, null, lieuId) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu(null, null, null, lieuId) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllByLieuIdAsync(lieuId);

        //Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByLieuIdAsync_WhenNoExistingEntities_ThenReturnEmpty()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByLieuIdAsync_WhenNoExistingEntities_ThenReturnEmpty))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        var lieuId = Guid.NewGuid();

        //Act
        var result = await sut.GetAllByLieuIdAsync(lieuId);

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GivenGetAllByStatutAsync_WhenExistingEntities_ThenReturnCorrectData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByStatutAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today, 
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null), PropositionStatut = PropositionStatut.Verifiee },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null), PropositionStatut = PropositionStatut.Verifiee },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null), PropositionStatut = PropositionStatut.Verifiee },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null), PropositionStatut = PropositionStatut.Verifiee }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllByStatutAsync(PropositionStatut.Verifiee, null, null);

        //Assert
        Assert.Equal(4, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByStatutAsync_WhenNoExistingEntities_ThenReturnEmptyData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByStatutAsync_WhenNoExistingEntities_ThenReturnEmptyData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null), PropositionStatut = PropositionStatut.AVerifier },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null), PropositionStatut = PropositionStatut.AVerifier },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null), PropositionStatut = PropositionStatut.AVerifier },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null), PropositionStatut = PropositionStatut.AVerifier },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null), PropositionStatut = PropositionStatut.AVerifier }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllByStatutAsync(PropositionStatut.Verifiee, null, null);

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GivenGetAllByUtilisateurIdAsync_WhenExistingEntities_ThenReturnCorrectData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByUtilisateurIdAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = utilisateurId.Value, DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = utilisateurId.Value, DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = utilisateurId.Value, DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllByUtilisateurIdAsync(utilisateurId);

        //Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GivenGetAllByUtilisateurIdAsync_WhenNoExistingEntities_ThenReturnEmptyData()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByUtilisateurIdAsync_WhenNoExistingEntities_ThenReturnEmptyData))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
        var utilisateurId = new UtilisateurId(Guid.NewGuid());

        //Act
        var result = await sut.GetAllByUtilisateurIdAsync(utilisateurId);

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GivenGetAllCountAsync_WhenCalled_ThenReturnCorrectNumber()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllCountAsync_WhenCalled_ThenReturnCorrectNumber))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null) },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null) }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllCountAsync();

        //Assert
        Assert.Equal(entities.Count, result);
    }

    [Fact]
    public async Task GivenGetAllCountByDecisionCodeAsync_WhenCalled_ThenReturnCorrectValue()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllCountByDecisionCodeAsync_WhenCalled_ThenReturnCorrectValue))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null")},
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null") }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllCountByDecisionCodeAsync(PropositionDecision.AccepterCode);

        //Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public async Task GivenGetAllCountByStatutAsync_WhenExistingEntities_ThenReturnCorrectValue()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllCountByStatutAsync_WhenExistingEntities_ThenReturnCorrectValue))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null), PropositionStatut = PropositionStatut.Verifiee },
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null), PropositionStatut = PropositionStatut.Verifiee },
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null), PropositionStatut = PropositionStatut.Verifiee },
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null), PropositionStatut = PropositionStatut.Verifiee }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllCountByStatutAsync(PropositionStatut.Verifiee);

        //Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public async Task GivenGetAllCountEnAttenteDecisionAsync_WhenCalled_ThenReturnCorrectValue()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllCountEnAttenteDecisionAsync_WhenCalled_ThenReturnCorrectValue))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null")},
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null") }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetAllCountEnAttenteDecisionAsync();

        //Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GivenGetByIdAsync_WhenCalled_ThenReturnCorrectValue()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetByIdAsync_WhenCalled_ThenReturnCorrectValue))
                .Options;

        using var dbContext = new AdministrationDbContext(options);
        var sut = new PropositionEvenementRepository(dbContext);
        var propositionEvenementId = new PropositionEvenementId(Guid.NewGuid());
        List<PropositionEvenementEntity> entities = [
            new PropositionEvenementEntity { Id = propositionEvenementId.Value, 
                Nom = "Nom1", Description = "Description1", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom1", "LieuDescription1", "LieuVille1", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom2", Description = "Description2", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom2", "LieuDescription2", "LieuVille2", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.AccepterCode, null)},
                new PropositionEvenementEntity { Nom = "Nom3", Description = "Description3", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom3", "LieuDescription3", "LieuVille3", null) ,
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null")},
                new PropositionEvenementEntity { Nom = "Nom4", Description = "Description4", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom4", "LieuDescription4", "LieuVille4", null) },
                new PropositionEvenementEntity { Nom = "Nom5", Description = "Description5", UtilisateurId = Guid.NewGuid(), DateDebut = DateTime.Today,
                DateFin = DateTime.Today.AddDays(1), Lieu = new PropositionLieu("LieuNom5", "LieuDescription5", "LieuVille5", null),
                Decision = new PropositionDecisionEntity(Guid.NewGuid(), DateTime.Today, PropositionDecision.RefuserCode, "null") }
            ];
        await dbContext.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        //Act
        var result = await sut.GetByIdAsync(propositionEvenementId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(propositionEvenementId, result.Id);
    }
}
