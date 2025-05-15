using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using System.Collections.Generic;
using System;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;

namespace Nodes.Api.Controllers.Nodes;

public class CreateNodeRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public int Importance { get; set; }
    public PhaseId PhaseId { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public TimelineId TimelineId { get; set; }
}

public class UpdateNodeRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public int Importance { get; set; }
    public PhaseId PhaseId { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public TimelineId TimelineId { get; set; }
}
