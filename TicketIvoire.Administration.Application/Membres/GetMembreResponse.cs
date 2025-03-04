namespace TicketIvoire.Administration.Application.Membres;

public record GetMembreResponse(Guid Id, 
    string Login, 
    string Email, 
    string Nom, 
    string Prenom, 
    string Telephone, 
    DateTime DateAdhesion, 
    bool EstActif, 
    EnumDto StatutAdhesion);
