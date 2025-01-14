// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Nodes.Application.Entities.Nodes.Commands.UpdateNode;

public class UpdateNodeCommand : ICommand<UpdateNodeResult>
{
    public required string Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public DateTime? Timestamp { get; init; }
    public int? Importance { get; init; }
    public string? Phase { get; init; }
    public List<string>? Categories { get; init; }
    public List<string>? Tags { get; init; }
}

public record UpdateNodeResult(bool NodeUpdated);

public class UpdateNodeCommandValidator : AbstractValidator<UpdateNodeCommand>
{
    public UpdateNodeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        // RuleFor(x => x.Title)
        //     .NotEmpty().When(x => x.Title is not null).WithMessage("Title is required.")
        //     .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        
        // RuleFor(x => x.Description)
        //     .NotEmpty().When(x => x.Title is not null).WithMessage("Description is required.")
        //     .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        
        // RuleFor(x => x.Timestamp)
        //     .LessThanOrEqualTo(DateTime.Now).When(x => x.Timestamp is not null)
        //     .WithMessage("Timestamp cannot be in the future.");
        
        // RuleFor(x => x.Importance)
        //     .InclusiveBetween(1, 10).When(x => x.Importance is not null)
        //     .WithMessage("Importance must be between 1 and 10.");
        
        // RuleFor(x => x.Phase)
        //     .NotEmpty().When(x => x.Phase is not null).WithMessage("Phase is required.");
    }
}
