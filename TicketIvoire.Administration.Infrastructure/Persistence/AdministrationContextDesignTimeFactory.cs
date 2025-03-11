using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TicketIvoire.Administration.Infrastructure.Persistence;

public class AdministrationContextDesignTimeFactory : IDesignTimeDbContextFactory<AdministrationDbContext>
{
    public AdministrationDbContext CreateDbContext(string[] args)
    {
        DbContextOptions<AdministrationDbContext> options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseNpgsql("DevConnection")
            .Options;
        return new AdministrationDbContext(options);
    }
}
