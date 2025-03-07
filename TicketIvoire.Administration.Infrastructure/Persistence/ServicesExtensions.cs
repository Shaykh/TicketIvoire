using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence;

public static class ServicesExtensions
{
    internal static IServiceCollection AddAdministrationPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdministrationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AdministrationDb")));
        services.AddScoped<IDbUnitOfWork, AdministrationDbContext>();
        services.AddRepositories();
        services.AddPersisterEventHandlers();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMembreRepository, MembreRepository>();
        services.AddScoped<IPropositionEvenementRepository, PropositionEvenementRepository>();
        services.AddScoped<ILieuRepository, LieuRepository>();
    }

    private static void AddPersisterEventHandlers(this IServiceCollection services)
    {
        services.AddLieuPersisterEventHandlers();
        services.AddMembrePersisterEventHandlers();
        services.AddPropositionEvenementPersisterEventHandlers();
    }
}
