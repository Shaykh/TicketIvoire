namespace TicketIvoire.Shared.Infrastructure.Persistence;

public class AuditableEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    public bool IsDeleted() => DeletedAt.HasValue;
    public Guid? DeletedBy { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public Guid? CreatedBy { get; protected set; }

    protected AuditableEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete(Guid deletedBy)
    {
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }

    public void Update(Guid updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

}
