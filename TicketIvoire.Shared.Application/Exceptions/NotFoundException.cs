namespace TicketIvoire.Shared.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundException()
    {
    }

    public NotFoundException(string entityId, string entityType) : base(SetMessage(entityId, entityType))
    {
    }

    private static string SetMessage(string entityId, string entityType)
        => $"L'entité de type {entityType} et d'identifiant {entityId} n'a pas été trouvée.";
}
