using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

public static class Mapping
{
    public static LieuEntity ToEntity(this Lieu lieu)
     => new()
     {
         Adresse = lieu.Adresse,
         Id = lieu.Id.Value,
         Nom = lieu.Nom,
         Description = lieu.Description,
         Capacite = lieu.Capacite,
         Ville = lieu.Ville,
         CoordonneesGeographiques = lieu.CoordonneesGeographiques
     };

    public static Lieu ToDomain(this LieuEntity lieuEntity)
     => new()
     {
         Id = new LieuId(lieuEntity.Id),
         Adresse = lieuEntity.Adresse,
         Nom = lieuEntity.Nom,
         Description = lieuEntity.Description,
         Capacite = lieuEntity.Capacite,
         Ville = lieuEntity.Ville,
         CoordonneesGeographiques = lieuEntity.CoordonneesGeographiques
     };
}
