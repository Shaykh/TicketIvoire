using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

public class LieuEntity : AuditableEntity
{
    public required string Nom { get; set; }
    public required string Description { get; set; }
    public required string Adresse { get; set; }
    public required string Ville { get; set; }
    public uint? Capacite { get; set; }
    public LieuCoordonneesGeographiques? CoordonneesGeographiques { get; set; }
    public string? RaisonsRetrait { get; set; }
}
