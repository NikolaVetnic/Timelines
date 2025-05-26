namespace BuildingBlocks.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public required T Id { get; init; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; protected set; } = false;
    public DateTime? DeletedAt { get; protected set; }
    public string? DeletedBy { get; protected set; }
}
