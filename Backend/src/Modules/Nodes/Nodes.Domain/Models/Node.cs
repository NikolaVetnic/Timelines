using Nodes.Domain.Events;

namespace Nodes.Domain.Models;

public class Node : Aggregate<NodeId>
{
    private readonly List<string> _categories = [];
    private readonly List<string> _tags = [];

    public IReadOnlyList<string> Categories => _categories.AsReadOnly();
    public IReadOnlyList<string> Tags => _tags.AsReadOnly();

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime Timestamp { get; set; }
    public required int Importance { get; set; }
    public required string Phase { get; set; }

    #region Node

    public static Node Create(NodeId id, string title, string description, string phase,
        DateTime timestamp, int importance, List<string> categories, List<string> tags)
    {
        var node = new Node
        {
            Id = id,
            Title = title,
            Description = description,
            Timestamp = timestamp,
            Importance = importance,
            Phase = phase
        };

        foreach (var category in categories)
            node.AddCategory(category);

        foreach (var tag in tags)
            node.AddTag(tag);

        node.AddDomainEvent(new NodeCreatedEvent(node));

        return node;
    }

    public void Update(string title, string description, DateTime timestamp, 
        int importance, string phase)
    {
        Title = title;
        Description = description;
        Timestamp = timestamp;
        Importance = importance;
        Phase = phase;

        AddDomainEvent(new NodeUpdatedEvent(this));
    }

    #endregion

    #region Categories

    private void AddCategory(string category)
    {
        _categories.Add(category);
    }

    private void RemoveCategory(string category)
    {
        _categories.Remove(category);
    }

    #endregion

    #region Tags

    private void AddTag(string tag)
    {
        _tags.Add(tag);
    }

    private void RemoveTag(string tag)
    {
        _tags.Remove(tag);
    }

    #endregion
}