using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Application.Membres.Command;
using TicketIvoire.Administration.Application.Membres.Query;
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
        services.AddMembreCommandsValidators();
        services.AddMembreQueriesValidators();
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


    private static void AddMembreCommandsValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AjouterMembreCommand>, AjouterMembreCommandValidator>();
        services.AddTransient<IValidator<DesactiverMembreCommand>, DesactiverMembreCommandValidator>();
        services.AddTransient<IValidator<ReactiverMembreCommand>, ReactiverMembreCommandValidator>();
        services.AddTransient<IValidator<RefuserMembreCommand>, RefuserMembreCommandValidator>();
        services.AddTransient<IValidator<ValiderMembreCommand>, ValiderMembreCommandValidator>();
    }

    private static void AddMembreQueriesValidators(this IServiceCollection services) 
        => services.AddTransient<IValidator<GetMembreByIdQuery>, GetMembreByIdQueryValidator>();
}
