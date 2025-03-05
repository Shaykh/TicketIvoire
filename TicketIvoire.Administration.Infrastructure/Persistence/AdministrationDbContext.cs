using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence;

public class AdministrationDbContext(DbContextOptions<AdministrationDbContext> options) : DbContext(options), IDbUnitOfWork
{
    public DbSet<MembreEntity> Membres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfiguration(new MembreEntityTypeConfiguration());
    public async Task CommitAsync(CancellationToken cancellationToken) => await SaveChangesAsync(cancellationToken);
}
