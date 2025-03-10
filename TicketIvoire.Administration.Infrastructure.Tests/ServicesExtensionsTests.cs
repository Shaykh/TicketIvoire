using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.Events;
using TicketIvoire.Shared.Infrastructure;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Tests;

public class ServicesExtensionsTests
{
    [Fact]
    public void GivenServiceCollection_WhenAddAdministrationInfrastructure_ThenAddUnitOfWork()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new ("ConnectionString:AdministrationDb", "AdministrationDbConnectionString")
                ])
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationInfrastructure(configuration);

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IUnitOfWork) && s.ImplementationType == typeof(UnitOfWork) && s.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddAdministrationInfrastructure_ThenAddDbContext()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new ("ConnectionString:AdministrationDb", "AdministrationDbConnectionString")
                ])
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationInfrastructure(configuration);

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDbUnitOfWork) && s.ImplementationType == typeof(AdministrationDbContext) && s.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddAdministrationInfrastructure_ThenAddRepositories()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new ("ConnectionString:AdministrationDb", "AdministrationDbConnectionString")
                ])
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationInfrastructure(configuration);

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(ILieuRepository) && s.ImplementationType == typeof(LieuRepository) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IMembreRepository) && s.ImplementationType == typeof(MembreRepository) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IPropositionEvenementRepository) && s.ImplementationType == typeof(PropositionEvenementRepository) && s.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddAdministrationInfrastructure_ThenAddLieuPersisterEventHandlers()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new ("ConnectionString:AdministrationDb", "AdministrationDbConnectionString")
                ])
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationInfrastructure(configuration);

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<LieuAjouteEvent>) && s.ImplementationType == typeof(LieuAjouteEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<LieuCoordonneesGeographiquesDefiniesEvent>) && s.ImplementationType == typeof(LieuCoordonneesGeographiquesDefiniesEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<LieuModifieEvent>) && s.ImplementationType == typeof(LieuModifieEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<LieuRetireEvent>) && s.ImplementationType == typeof(LieuRetireEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddAdministrationInfrastructure_ThenAddMembrePersisterEventHandlers()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new ("ConnectionString:AdministrationDb", "AdministrationDbConnectionString")
                ])
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationInfrastructure(configuration);

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<MembreCreeEvent>) && s.ImplementationType == typeof(MembreCreeEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<MembreDesactiveEvent>) && s.ImplementationType == typeof(MembreDesactiveEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<MembreReactiveEvent>) && s.ImplementationType == typeof(MembreReactiveEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<MembreRefuseEvent>) && s.ImplementationType == typeof(MembreRefuseEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<MembreValideEvent>) && s.ImplementationType == typeof(MembreValideEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddAdministrationInfrastructure_ThenAddPropositionEvenementPersisterEventHandlers()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new ("ConnectionString:AdministrationDb", "AdministrationDbConnectionString")
                ])
            .Build();
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationInfrastructure(configuration);

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<PropositionEvenementAccepteeEvent>) && s.ImplementationType == typeof(PropositionEvenementAccepteeEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<PropositionEvenementCreeEvent>) && s.ImplementationType == typeof(PropositionEvenementCreeEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<PropositionEvenementRefuseeEvent>) && s.ImplementationType == typeof(PropositionEvenementRefuseeEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventHandler<PropositionEvenementVerifieeEvent>) && s.ImplementationType == typeof(PropositionEvenementVerifieeEventHandler) && s.Lifetime == ServiceLifetime.Scoped);
    }
}
