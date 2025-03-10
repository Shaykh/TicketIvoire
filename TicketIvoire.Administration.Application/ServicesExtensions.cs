using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Application.LieuEvenements.Command;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Application.Membres.Command;
using TicketIvoire.Administration.Application.Membres.Query;
using TicketIvoire.Administration.Application.PropositionEvenements.Command;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
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
        services.AddPropositionCommandsValidators();
        services.AddPropositionQueriesValidators();
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


    private static void AddPropositionCommandsValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AccepterPropositionEvenementCommand>, AccepterPropositionEvenementCommandValidator>();
        services.AddTransient<IValidator<AjouterPropositionEvenementCommand>, AjouterPropositionEvenementCommandValidator>();
        services.AddTransient<IValidator<RefuserPropositionEvenementCommand>, RefuserPropositionEvenementCommandValidator>();
        services.AddTransient<IValidator<VerifierPropositionEvenementCommand>, VerifierPropositionEvenementCommandValidator>();
    }

    private static void AddPropositionQueriesValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<GetAllPropositionsByDateRangeQuery>, GetAllPropositionsByDateRangeQueryValidator>();
        services.AddTransient<IValidator<GetAllPropositionsByDecisionQuery>, GetAllPropositionsByDecisionQueryValidator>();
        services.AddTransient<IValidator<GetAllPropositionsByLieuIdQuery>, GetAllPropositionsByLieuIdQueryValidator>();
        services.AddTransient<IValidator<GetAllPropositionsByUtilisateurIdQuery>, GetAllPropositionsByUtilisateurIdQueryValidator>();
        services.AddTransient<IValidator<GetNombreAllPropositionsByDecisionQuery>, GetNombreAllPropositionsByDecisionQueryValidator>();
        services.AddTransient<IValidator<GetPropositionEvenementByIdQuery>, GetPropositionEvenementByIdQueryValidator>();
    }
}
