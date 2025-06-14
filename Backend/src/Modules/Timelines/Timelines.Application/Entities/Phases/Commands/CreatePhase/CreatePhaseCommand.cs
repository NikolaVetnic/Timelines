﻿using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Application.Entities.Phases.Commands.CreatePhase;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreatePhaseCommand : ICommand<CreatePhaseResult>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Duration { get; set; }
    public required string Status { get; set; }
    public required decimal Progress { get; set; }
    public required bool IsCompleted { get; set; }
    public required List<PhaseId> DependsOn { get; set; }
    public required string AssignedTo { get; set; }
    public required List<string> Stakeholders { get; set; } = [];
    public required List<string> Tags { get; set; } = [];
    public required TimelineId TimelineId { get; set; }
}

public record CreatePhaseResult(PhaseId Id);

public class CreatePhaseCommandValidator : AbstractValidator<CreatePhaseCommand>
{
    public CreatePhaseCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
    }
}
