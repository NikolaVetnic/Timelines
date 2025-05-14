using System.Collections.Generic;
using System;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Notes.Api.Controllers.Notes;

public record CreateNoteRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string Owner { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
    public NodeId NodeId { get; set; }
}

public record UpdateNoteRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
    public NodeId NodeId { get; set; }
}
