namespace Nodes.Application.Entities.Nodes.Extensions;

public static class NodeExtensions
{
    public static NodeDto ToNodeDto(this Node node)
    {
        return new NodeDto(
            node.Id.ToString(),
            node.Title,
            node.Description,
            node.Timestamp,
            node.Importance,
            node.Phase,
            node.Categories.ToList(),
            node.Tags.ToList());
    }

    public static IEnumerable<NodeDto> ToNodeDtoList(this IEnumerable<Node> nodes)
    {
        return nodes.Select(ToNodeDto);
    }
}
