using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Shared.Infrastructure.Tests.Persistence;

public class AuditableEntityTests
{
    [Fact]
    public void GivenDelete_WhenCalled_ThenSetsDeletedAtAndDeletedBy()
    {
        // Arrange
        var createdBy = Guid.NewGuid();
        var auditableEntity = new AuditableEntity(createdBy);
        var deletedBy = Guid.NewGuid();

        // Act
        auditableEntity.Delete(deletedBy);

        // Assert
        Assert.Equal(createdBy, auditableEntity.CreatedBy);
        Assert.NotNull(auditableEntity.DeletedAt);
        Assert.True(auditableEntity.IsDeleted());
        Assert.Equal(deletedBy, auditableEntity.DeletedBy);
    }

    [Fact]
    public void GivenUpdate_WhenCalled_ThenSetsUpdatedAtAndUpdatedBy()
    {
        // Arrange
        var auditableEntity = new AuditableEntity();
        var updatedBy = Guid.NewGuid();

        // Act
        auditableEntity.Update(updatedBy);

        // Assert
        Assert.Equal(updatedBy, auditableEntity.UpdatedBy);
    }
}
