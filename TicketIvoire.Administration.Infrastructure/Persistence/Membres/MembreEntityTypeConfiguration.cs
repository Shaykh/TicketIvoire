using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres;

public class MembreEntityTypeConfiguration : IEntityTypeConfiguration<MembreEntity>
{
    public void Configure(EntityTypeBuilder<MembreEntity> builder)
    {
        builder.ToTable("Membres");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedNever();
        builder.Property(m => m.Login)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(m => m.Nom)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(m => m.Prenom)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(m => m.Telephone)
            .IsRequired()
            .HasMaxLength(18);
        builder.Property(m => m.DateAdhesion)
            .IsRequired();
        builder.Property(m => m.StatutAdhesion)
            .HasConversion<short>();
    }
}
