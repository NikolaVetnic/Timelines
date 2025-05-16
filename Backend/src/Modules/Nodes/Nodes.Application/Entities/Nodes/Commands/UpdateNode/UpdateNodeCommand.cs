using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Application.Entities.Nodes.Commands.UpdateNode;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public record UpdateNodeCommand : ICommand<UpdateNodeResult>
{
    public required NodeId Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public PhaseId PhaseId { get; set; }
    public DateTime? Timestamp { get; set; }
    public int? Importance { get; set; }
    public List<string>? Categories { get; set; }
    public List<string>? Tags { get; set; }
    public TimelineId? TimelineId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateNodeResult(NodeBaseDto Node);

public class UpdateNodeCommandValidator : AbstractValidator<UpdateNodeCommand>
{
    public UpdateNodeCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Timestamp)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Timestamp cannot be in the future.")
            .When(x => x.Timestamp is not null);

        RuleFor(x => x.Importance)
            .InclusiveBetween(1, 10).WithMessage("Importance must be between 1 and 10.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Categories)
            .Must(categories => categories is { Count: > 0 })
            .WithMessage("At least one category must be provided.")
            .When(x => x.Categories is not null);

        RuleFor(x => x.Tags)
            .Must(tags => tags is { Count: > 0 })
            .WithMessage("At least one tag must be provided.")
            .When(x => x.Tags is not null);

        RuleForEach(x => x.Categories)
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.")
            .When(x => x.Categories is { Count: > 0 });

        RuleForEach(x => x.Tags)
            .MaximumLength(50).WithMessage("Tag must not exceed 50 characters.")
            .When(x => x.Tags is { Count: > 0 });
    }
}
