using TicketIvoire.Administration.Domain.Membres;

namespace TicketIvoire.Administration.Application.Membres;

public static class Mapping
{
    public static GetMembreResponse ToResponse(this Membre membre)
        => new (membre.Id.Value,
            membre.Login,
            membre.Email,
            membre.Nom,
            membre.Prenom,
            membre.Telephone,
            membre.DateAdhesion,
            membre.EstActif,
            membre.StatutAdhesion.ToDto());

    public static EnumDto ToDto(this StatutAdhesion statutAdhesion)
        => new ((int)statutAdhesion, statutAdhesion.GetLabel());

    public static string GetLabel(this StatutAdhesion statutAdhesion)
        => statutAdhesion switch
        {
            StatutAdhesion.Accepte => "Accepté",
            StatutAdhesion.Refuse => "Refusé",
            _ => throw new ArgumentOutOfRangeException(nameof(statutAdhesion), statutAdhesion, null)
        };
}
