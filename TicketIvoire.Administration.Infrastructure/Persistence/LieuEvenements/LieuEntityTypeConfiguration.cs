using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

public class LieuEntityTypeConfiguration : IEntityTypeConfiguration<LieuEntity>
{
    public void Configure(EntityTypeBuilder<LieuEntity> builder)
    {
        builder.ToTable("LieuEvenements", "Administration");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever();
        builder.Property(e => e.Nom)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Adresse)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(e => e.Ville)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(800);
        builder.OwnsOne(e => e.CoordonneesGeographiques, cg =>
        {
            cg.Property(c => c.Latitude)
                .HasColumnName("Latitude")
                .IsRequired();
            cg.Property(c => c.Longitude)
                .HasColumnName("Longitude")
                .IsRequired();
        });
    }
}
