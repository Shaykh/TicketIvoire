using System.Diagnostics.CodeAnalysis;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Domain.LieuEvenements.Rules;
using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Administration.Domain.LieuEvenements;

public class Lieu : EntityBase, IAggregateRoot
{
    public required LieuId Id { get; set; }
    public required string Nom { get; set; }
    public required string Description { get; set; }
    public required string Adresse { get; set; }
    public required string Ville { get; set; }
    public uint? Capacite { get; set; }
    public LieuCoordonneesGeographiques? CoordonneesGeographiques { get; set; }

    [SetsRequiredMembers]
    private Lieu(string nom, string description, string adresse, string ville, uint? capacite)
    {
        Id = new LieuId(Guid.NewGuid());
        Nom = nom;
        Description = description;
        Adresse = adresse;
        Ville = ville;
        Capacite = capacite;
        RegisterEvent(new LieuAjouteEvent(Id.Value, Capacite, Nom, Description, Adresse, Ville));
    }

    public static Lieu Create(string nom, string description, string adresse, string ville, uint? capacite)
    {
        CheckRule(new LieuInformationMustBeValidRule(nom, description, adresse, ville));
        var newLieu = new Lieu(nom, description, adresse, ville, capacite);
        return newLieu;
    }

    public void Modifier(string nom, string description, string adresse, string ville, uint? capacite)
    {
        CheckRule(new LieuInformationMustBeValidRule(nom, description, adresse, ville));
        Nom = nom;
        Description = description;
        Adresse = adresse;
        Ville = ville;
        Capacite = capacite;
        RegisterEvent(new LieuModifieEvent(Id.Value, Capacite, Nom, Description, Adresse, Ville));
    }

    public void DefinirCoordonneesGeographiques(LieuCoordonneesGeographiques coordonneesGeographiques)
    {
        CheckRule(new CoordonneesGeographiquesMustBeValidRule(coordonneesGeographiques.Latitude, coordonneesGeographiques.Longitude));
        CoordonneesGeographiques = coordonneesGeographiques;
        RegisterEvent(new LieuCoordonneesGeographiquesDefiniesEvent(Id.Value, CoordonneesGeographiques.Latitude, CoordonneesGeographiques.Longitude));
    }

    public void Supprimer(string raisons)
    {
        CheckRule(new SuppressionRaisonsMustBeValidRule(raisons));
        RegisterEvent(new LieuRetireEvent(Id.Value, raisons));
    }
}
