using BuildingBlocks.Domain.Exceptions;

namespace Nodes.Domain.ValueObjects.Ids;

public record PhaseId
{
    private PhaseId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PhaseId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("PhaseId cannot be empty.");

        return new PhaseId(value);
    }
}
