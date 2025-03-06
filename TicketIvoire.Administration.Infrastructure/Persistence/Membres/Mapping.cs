using TicketIvoire.Administration.Domain.Membres;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres;

public static class Mapping
{
    public static MembreEntity ToEntity(this Membre membre) => new()
    {
        Id = membre.Id.Value,
        Nom = membre.Nom,
        Prenom = membre.Prenom,
        Email = membre.Email,
        Telephone = membre.Telephone,
        Login = membre.Login,
        DateAdhesion = membre.DateAdhesion
    };

    public static Membre ToDomain(this MembreEntity membreEntity) => new ()
    {
        Id = new MembreId(membreEntity.Id),
        Login = membreEntity.Login,
        Email = membreEntity.Email,
        Nom = membreEntity.Nom,
        Prenom = membreEntity.Prenom,
        Telephone = membreEntity.Telephone,
        DateAdhesion = membreEntity.DateAdhesion
    };
}
