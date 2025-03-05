using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres;

public class MembreEntityTypeConfiguration : IEntityTypeConfiguration<MembreEntity>
{
    public void Configure(EntityTypeBuilder<MembreEntity> builder) => builder.ToTable("Membres");
}
