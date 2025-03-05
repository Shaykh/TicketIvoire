using System.Diagnostics.CodeAnalysis;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Administration.Domain.Membres.Rules;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.Membres;

public class Membre : EntityBase, IAggregateRoot
{
    public required MembreId Id { get; set; }
    public required string Login { get; set; }
    public required string Email { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Telephone { get; set; }
    public required DateTime DateAdhesion { get; set; }
    public bool EstActif { get; set; }
    public StatutAdhesion StatutAdhesion { get; set; }

    [SetsRequiredMembers]
    private Membre(string login, string email, string nom, string prenom, string telephone, DateTime dateAdhesion)
    {
        Id = new MembreId(Guid.NewGuid());
        Login = login;
        Email = email;
        Nom = nom;
        Prenom = prenom;
        Telephone = telephone;
        DateAdhesion = dateAdhesion;
        EstActif = true;
        RegisterEvent(new MembreCreeEvent(Id.Value, Login, Email, Nom, Prenom, Telephone, DateAdhesion));
    }

    public static Membre Create(string login, string email, string nom, string prenom, string telephone, DateTime dateAdhesion)
    {
        CheckRule(new LoginMustBeValidRule(login));
        CheckRule(new EmailMustBeValidRule(email));
        CheckRule(new TelephoneMustBeValidRule(telephone));
        var newMembre = new Membre(login, email, nom, prenom, telephone, dateAdhesion);

        return newMembre;
    }

    public void Valider()
    {
        StatutAdhesion = StatutAdhesion.Accepte;
        RegisterEvent(new MembreValideEvent(Id.Value));
    }

    public void Refuser()
    {
        StatutAdhesion = StatutAdhesion.Refuse;
        RegisterEvent(new MembreRefuseEvent(Id.Value));
    }

    public void Desactiver(string raisons)
    {
        EstActif = false;
        RegisterEvent(new MembreDesactiveEvent(Id.Value, raisons));
    }

    public void Reactiver(string raisons)
    {
        EstActif = true;
        RegisterEvent(new MembreReactiveEvent(Id.Value, raisons));
    }
}
