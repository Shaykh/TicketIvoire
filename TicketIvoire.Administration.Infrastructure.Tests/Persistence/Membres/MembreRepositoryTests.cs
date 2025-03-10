using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.Membres
{
    public class MembreRepositoryTests
    {
        [Fact]
        public async Task GivenGetAllAsync_WhenNumberByPageNullAndPageNumberNull_ThenReturnAllData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllAsync_WhenNumberByPageNullAndPageNumberNull_ThenReturnAllData))
                .Options;

            using var dbContext = new AdministrationDbContext(options);
            var sut = new MembreRepository(dbContext);
            List<MembreEntity> entities =
            [
                new MembreEntity { Login = "Login1", Email = "Email1", Nom = "Nom1", Prenom = "Prenom1", Telephone = "Telephone1", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login2", Email = "Email2", Nom = "Nom2", Prenom = "Prenom2", Telephone = "Telephone2", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login3", Email = "Email3", Nom = "Nom3", Prenom = "Prenom3", Telephone = "Telephone3", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte }
            ];
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
            uint? pageNumber = null;
            uint? numberByPage = null;

            // Act
            var result = await sut.GetAllAsync(pageNumber, numberByPage);

            // Assert
            Assert.Equal(entities.Count, result.Count());
        }

        [Fact]
        public async Task GivenGetAllAsync_WhenNumberByPageNotNullAndPageNumberNotNull_ThenReturnNumberByPageData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllAsync_WhenNumberByPageNotNullAndPageNumberNotNull_ThenReturnNumberByPageData))
                .Options;

            using var dbContext = new AdministrationDbContext(options);
            var sut = new MembreRepository(dbContext);
            List<MembreEntity> entities =
            [
                new MembreEntity { Login = "Login1", Email = "Email1", Nom = "Nom1", Prenom = "Prenom1", Telephone = "Telephone1", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login2", Email = "Email2", Nom = "Nom2", Prenom = "Prenom2", Telephone = "Telephone2", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login3", Email = "Email3", Nom = "Nom3", Prenom = "Prenom3", Telephone = "Telephone3", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte }
            ];
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
            uint? pageNumber = 1;
            uint? numberByPage = 2;

            // Act
            var result = await sut.GetAllAsync(pageNumber, numberByPage);

            // Assert
            Assert.Equal((int)numberByPage.Value, result.Count());
        }

        [Fact]
        public async Task GivenGetAllByStatutAdhesionAsync_WhenExistingEntities_ThenReturnCorrectData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllByStatutAdhesionAsync_WhenExistingEntities_ThenReturnCorrectData))
                .Options;

            using var dbContext = new AdministrationDbContext(options);
            var sut = new MembreRepository(dbContext);
            List<MembreEntity> entities =
            [
                new MembreEntity { Login = "Login1", Email = "Email1", Nom = "Nom1", Prenom = "Prenom1", Telephone = "Telephone1", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login2", Email = "Email2", Nom = "Nom2", Prenom = "Prenom2", Telephone = "Telephone2", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Refuse },
                new MembreEntity { Login = "Login3", Email = "Email3", Nom = "Nom3", Prenom = "Prenom3", Telephone = "Telephone3", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte }
            ];
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await sut.GetAllByStatutAdhesionAsync(StatutAdhesion.Accepte, null, null);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GivenGetByIdAsync_WhenCalled_ThenReturnCorrectValue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetByIdAsync_WhenCalled_ThenReturnCorrectValue))
                .Options;

            using var dbContext = new AdministrationDbContext(options);
            var sut = new MembreRepository(dbContext);
            var membreId = new MembreId(Guid.NewGuid());
            List<MembreEntity> entities =
            [
                new MembreEntity { Id = membreId.Value, Login = "Login1", Email = "Email1", Nom = "Nom1", Prenom = "Prenom1", Telephone = "Telephone1", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login2", Email = "Email2", Nom = "Nom2", Prenom = "Prenom2", Telephone = "Telephone2", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Refuse }
            ];
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await sut.GetByIdAsync(membreId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(membreId, result.Id);
        }

        [Fact]
        public async Task GivenGetAllCountAsync_WhenCalled_ThenReturnCorrectNumber()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetAllCountAsync_WhenCalled_ThenReturnCorrectNumber))
                .Options;

            using var dbContext = new AdministrationDbContext(options);
            var sut = new MembreRepository(dbContext);
            List<MembreEntity> entities =
            [
                new MembreEntity { Login = "Login1", Email = "Email1", Nom = "Nom1", Prenom = "Prenom1", Telephone = "Telephone1", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login2", Email = "Email2", Nom = "Nom2", Prenom = "Prenom2", Telephone = "Telephone2", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Refuse }
            ];
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await sut.GetAllCountAsync();

            // Assert
            Assert.Equal(entities.Count, result);
        }

        [Fact]
        public async Task GivenGetCountByStatutAdhesionAsync_WhenCalled_ThenReturnCorrectValue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AdministrationDbContext>()
                .UseInMemoryDatabase(nameof(GivenGetCountByStatutAdhesionAsync_WhenCalled_ThenReturnCorrectValue))
                .Options;

            using var dbContext = new AdministrationDbContext(options);
            var sut = new MembreRepository(dbContext);
            List<MembreEntity> entities =
            [
                new MembreEntity { Login = "Login1", Email = "Email1", Nom = "Nom1", Prenom = "Prenom1", Telephone = "Telephone1", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte },
                new MembreEntity { Login = "Login2", Email = "Email2", Nom = "Nom2", Prenom = "Prenom2", Telephone = "Telephone2", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Refuse },
                new MembreEntity { Login = "Login3", Email = "Email3", Nom = "Nom3", Prenom = "Prenom3", Telephone = "Telephone3", DateAdhesion = DateTime.Today, StatutAdhesion = StatutAdhesion.Accepte }
            ];
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await sut.GetCountByStatutAdhesionAsync(StatutAdhesion.Accepte);

            // Assert
            Assert.Equal(2, result);
        }
    }
}
