using Nodes.Domain.Events;

namespace Nodes.Domain.Models;

public class Node : Aggregate<NodeId>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Phase { get; set; }
        public required string[] Categories { get; set; }
        public required string[] Tags { get; set; }
    }
}
