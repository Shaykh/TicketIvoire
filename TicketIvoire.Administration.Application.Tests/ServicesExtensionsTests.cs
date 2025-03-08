using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
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
}
