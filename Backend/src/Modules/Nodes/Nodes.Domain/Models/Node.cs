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
        public required string Phase { get; set; }
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
