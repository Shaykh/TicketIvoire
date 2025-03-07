using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

public class PropositionEvenementEntity : AuditableEntity
{
    public required string Nom { get; set; }
    public required string Description { get; set; }
    public required DateTime DateDebut { get; set; }
    public required DateTime DateFin { get; set; }
    public PropositionStatut PropositionStatut { get; set; } = PropositionStatut.AVerifier;
    public required PropositionLieu Lieu { get; set; }
    public required Guid UtilisateurId { get; set; }
    public PropositionDecision? Decision { get; set; }
}
