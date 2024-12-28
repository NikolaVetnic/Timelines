using System.Text.Json;
using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects.Ids;

public record NodeId
{
    private NodeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static NodeId Of(Guid value)
    {
        if (value == Guid.Empty) throw new EmptyIdException("NodeId cannot be empty.");

        return new NodeId(value);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

