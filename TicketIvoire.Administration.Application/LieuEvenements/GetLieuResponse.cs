namespace TicketIvoire.Administration.Application.LieuEvenements;

public record GetLieuResponse(Guid Id,
    string Nom,
    string Description,
    string Ville,
    string Adresse,
    uint? Capacite,
    CoordonneesGeoDto? CoordonneesGeo);
