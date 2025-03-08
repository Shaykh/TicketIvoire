using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Shared.Application;

namespace TicketIvoire.Administration.Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddAdministrationApplication(this IServiceCollection services)
    {
        services.AddSharedApplication();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(ServicesExtensions).Assembly));
        services.AddLieuEvenementCommandsValidators();
        services.AddLieuEvenementQueriesValidators();
        return services;
    }

    private static void AddLieuEvenementCommandsValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AjouterLieuCommand>, AjouterLieuCommandValidator>();
        services.AddTransient<IValidator<DefinirCoordonneesGeographiquesCommand>, DefinirCoordonneesGeographiquesCommandValidator>();
        services.AddTransient<IValidator<ModifierLieuCommand>, ModifierLieuCommandValidator>();
        services.AddTransient<IValidator<RetirerLieuCommand>, RetirerLieuCommandValidator>();
    }

    private static void AddLieuEvenementQueriesValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<GetAllLieuxByVilleQuery>, GetAllLieuxByVilleQueryValidator>();
        services.AddTransient<IValidator<GetLieuByIdQuery>, GetLieuByIdQueryValidator>();
        services.AddTransient<IValidator<GetLieuxByCapaciteRangeQuery>, GetLieuxByCapaciteRangeQueryValidator>();
    }
}
