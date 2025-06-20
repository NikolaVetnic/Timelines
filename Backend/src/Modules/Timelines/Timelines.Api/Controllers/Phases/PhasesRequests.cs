﻿using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using System;

namespace Timelines.Api.Controllers.Phases;

public class CreatePhaseRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? Duration { get; set; }
    public string Status { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted { get; set; }
    public PhaseId Parent { get; set; }
    public PhaseId[] DependsOn { get; set; }
    public string AssignedTo { get; set; }
    public string[] Stakeholders { get; set; }
    public string[] Tags { get; set; }
    public TimelineId TimelineId { get; set; }
}

public class UpdatePhaseRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? Duration { get; set; }
    public string Status { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted { get; set; }
    public PhaseId Parent { get; set; }
    public PhaseId[] DependsOn { get; set; }
    public string AssignedTo { get; set; }
    public string[] Stakeholders { get; set; }
    public string[] Tags { get; set; }
    public TimelineId TimelineId { get; set; }
}
