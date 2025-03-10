using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Application.Membres.Command;
using TicketIvoire.Administration.Application.Membres.Query;
using TicketIvoire.Administration.Application.PropositionEvenements.Command;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Shared.Application.Events;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Application.Tests;

public class ServicesExtensionsTests
{
    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddMediatr()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IMediator));
    }

    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddEventsManagers()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventsContainer) && s.ImplementationType == typeof(DomainEventsContainer) && s.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, s => s.ServiceType == typeof(IDomainEventsDispatcher) && s.ImplementationType == typeof(DomainEventsDispatcher) && s.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddLieuEvenementCommandsValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<AjouterLieuCommand>)  && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<DefinirCoordonneesGeographiquesCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<ModifierLieuCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<RetirerLieuCommand>) && s.Lifetime == ServiceLifetime.Transient);
    }


    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddLieuEvenementQueriesValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetAllLieuxByVilleQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetLieuByIdQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetLieuxByCapaciteRangeQuery>) && s.Lifetime == ServiceLifetime.Transient);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddMembreCommandsValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<AjouterMembreCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<DesactiverMembreCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<ReactiverMembreCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<RefuserMembreCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<ValiderMembreCommand>) && s.Lifetime == ServiceLifetime.Transient);
    }


    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddMembreQueriesValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetMembreByIdQuery>) && s.Lifetime == ServiceLifetime.Transient);
    }

    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddPropositionCommandsValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<AccepterPropositionEvenementCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<AjouterPropositionEvenementCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<RefuserPropositionEvenementCommand>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<VerifierPropositionEvenementCommand>) && s.Lifetime == ServiceLifetime.Transient);
    }


    [Fact]
    public void GivenServiceCollection_WhenAddSharedApplication_ThenAddPropositionQueriesValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdministrationApplication();

        // Assert
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetAllPropositionsByDateRangeQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetAllPropositionsByDecisionQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetAllPropositionsByLieuIdQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetAllPropositionsByUtilisateurIdQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetNombreAllPropositionsByDecisionQuery>) && s.Lifetime == ServiceLifetime.Transient);
        Assert.Contains(services, s => s.ServiceType == typeof(IValidator<GetPropositionEvenementByIdQuery>) && s.Lifetime == ServiceLifetime.Transient);
    }

}
