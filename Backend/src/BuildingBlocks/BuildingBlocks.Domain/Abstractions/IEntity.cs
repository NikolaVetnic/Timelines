namespace BuildingBlocks.Domain.Abstractions;

public interface IEntity<out T> : IEntity
{
    T Id { get; }
}

public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
