using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

public static class Mapping
{
    public static LieuEntity ToEntity(this LieuAjouteEvent lieu)
     => new()
     {
         Adresse = lieu.Adresse,
         Id = lieu.LieuId,
         Nom = lieu.Nom,
         Description = lieu.Description,
         Capacite = lieu.Capacite,
         Ville = lieu.Ville
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

    public static void UpdateTo(this LieuEntity entity, LieuModifieEvent lieuModifieEvent)
    {
        entity.Adresse = lieuModifieEvent.Adresse;
        entity.Nom = lieuModifieEvent.Nom;
        entity.Description = lieuModifieEvent.Description;
        entity.Capacite = lieuModifieEvent.Capacite;
        entity.Ville = lieuModifieEvent.Ville;
        entity.Update(Guid.Empty);
    }
}
