using Nodes.Domain.Exceptions;

namespace Nodes.Domain.ValueObjects.Ids
{
    public record NodeId
    {
        private NodeId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static NodeId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty) throw new DomainException("NodeId cannot be empty.");

            return new NodeId(value);
        }
    }
}
