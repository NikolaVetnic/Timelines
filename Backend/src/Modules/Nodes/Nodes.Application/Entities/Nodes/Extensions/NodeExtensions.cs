using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Phase.Dtos;

namespace Nodes.Application.Entities.Nodes.Extensions;

public static class NodeExtensions
{
    public static NodeDto ToNodeDto(this Node node, PhaseBaseDto phase)
    {
        return new NodeDto(
            node.Id.ToString(),
            node.Title,
            node.Description,
            node.Timestamp,
            node.Importance,
            node.Categories.ToList(),
            node.Tags.ToList(),
            phase);
    }
}
