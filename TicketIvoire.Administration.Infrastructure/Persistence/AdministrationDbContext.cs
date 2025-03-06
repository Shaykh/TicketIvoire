using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence;

public class AdministrationDbContext(DbContextOptions<AdministrationDbContext> options) : DbContext(options), IDbUnitOfWork
{
    public DbSet<MembreEntity> Membres { get; set; }
    public DbSet<PropositionEvenementEntity> PropositionEvenements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MembreEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PropositionEvenementEntityTypeConfiguration());
    }

    public async Task CommitAsync(CancellationToken cancellationToken) => await SaveChangesAsync(cancellationToken);
}
