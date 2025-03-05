using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.LieuEvenements;

public static class Mapping
{
    public static GetLieuResponse ToGetLieuResponse(this Lieu lieu) => new (lieu.Id.Value, 
        lieu.Nom, 
        lieu.Description, 
        lieu.Ville, 
        lieu.Adresse, 
        lieu.Capacite,
        lieu.CoordonneesGeographiques?.ToDto());

    public static CoordonneesGeoDto ToDto(this LieuCoordonneesGeographiques coordonneesGeographiques) 
        => new(coordonneesGeographiques.Latitude, coordonneesGeographiques.Longitude);
}
