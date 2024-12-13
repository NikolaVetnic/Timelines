using BuildingBlocks.Domain;
using Nodes.Domain.ValueObjects.Ids;

namespace Nodes.Domain.Models
{
    public class Node : Entity<NodeId>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Phase Phase { get; set; }
        public required string[] Categories { get; set; }
        public required string[] Tags { get; set; }
    }
}
