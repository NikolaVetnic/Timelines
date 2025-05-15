using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Nodes.Application.Entities.Nodes.Commands.CreateNode;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateNodeCommand : ICommand<CreateNodeResult>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required PhaseId PhaseId { get; set; }
    public required DateTime Timestamp { get; set; }
    public required int Importance { get; set; }
    public required List<string> Categories { get; set; }
    public required List<string> Tags { get; set; }
    public required TimelineId TimelineId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateNodeResult(NodeId Id);

public class CreateNodeCommandValidator : AbstractValidator<CreateNodeCommand>
{
    public CreateNodeCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Timestamp)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Timestamp cannot be in the future.");

        RuleFor(x => x.Importance)
            .InclusiveBetween(1, 10).WithMessage("Importance must be between 1 and 10.");

        RuleFor(x => x.PhaseId)
            .NotEmpty().WithMessage("Phase is required.");
        
        RuleFor(x => x)
            .NotNull().WithMessage("Node cannot be null.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Categories)
                    .Must(categories => categories != null && categories.Count > 0)
                    .WithMessage("At least one category must be provided.");
                
                RuleFor(x => x.Tags)
                    .Must(tags => tags != null && tags.Count > 0)
                    .WithMessage("At least one tag must be provided.");
            });

        RuleForEach(x => x.Categories)
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

        RuleForEach(x => x.Tags)
            .MaximumLength(50).WithMessage("Tag must not exceed 50 characters.");
    }
}
