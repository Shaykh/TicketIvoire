using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

public class PropositionEvenementEntityTypeConfiguration : IEntityTypeConfiguration<PropositionEvenementEntity>
{
    public void Configure(EntityTypeBuilder<PropositionEvenementEntity> builder)
    {
        builder.ToTable("PropositionEvenements");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Nom)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(e => e.DateDebut)
            .IsRequired();
        builder.Property(e => e.DateFin)
            .IsRequired();
        builder.Property(e => e.PropositionStatut)
            .HasConversion<short>();
        builder.Property(e => e.UtilisateurId).IsRequired();
        builder.OwnsOne(e => e.Lieu, lieu =>
        {
            lieu.Property(l => l.Description)
                .HasMaxLength(200);
            lieu.Property(l => l.Nom)
                .HasMaxLength(100);
            lieu.Property(l => l.Ville)
                .HasMaxLength(50);
        });
        builder.OwnsOne(e => e.Decision, dec =>
        {
            dec.Property(d => d.DateDecision)
                .IsRequired();
            dec.Property(d => d.Raisons)
                .HasMaxLength(1000);
            dec.Property(d => d.Code)
                .IsRequired();
            dec.Property(d => d.UtilisateurId)
                .IsRequired();
        });
        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}
