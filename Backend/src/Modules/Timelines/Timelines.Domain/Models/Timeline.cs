using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.Events;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Domain.Models;

public class Timeline : Aggregate<TimelineId>
{
    public required string Title { get; set; }

    public List<NodeId> NodeIds { get; set; } = [];

    #region Timeline

    public static Timeline Create(TimelineId id, string title)
    {
        var timeline = new Timeline
        {
            Id = id,
            Title = title
        };

        timeline.NodeIds = [];

        timeline.AddDomainEvent(new TimelineCreatedEvent(timeline.Id));

        return timeline;
    }

    public void Update(string title)
    {
        Title = title;

        AddDomainEvent(new TimelineUpdatedEvent(Id));
    }

    #endregion

    #region Nodes

    public void AddNode(NodeId nodeId)
    {
        if (!NodeIds.Contains(nodeId))
            NodeIds.Add(nodeId);
    }

    public void RemoveNode(NodeId nodeId)
    {
        if (NodeIds.Contains(nodeId))
            NodeIds.Remove(nodeId);
    }

    #endregion
}
