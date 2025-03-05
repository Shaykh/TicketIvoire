using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres;

public class MembreEntity : AuditableEntity
{
    public required string Login { get; set; }
    public required string Email { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Telephone { get; set; }
    public required DateTime DateAdhesion { get; set; }
    public bool EstActif { get; set; }
    public StatutAdhesion StatutAdhesion { get; set; }
}
