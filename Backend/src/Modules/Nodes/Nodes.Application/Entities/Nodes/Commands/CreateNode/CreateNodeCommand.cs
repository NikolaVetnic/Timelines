using Nodes.Application.Entities.Nodes.Dtos;

namespace Nodes.Application.Entities.Nodes.Commands.CreateNode;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateNodeCommand(NodeDto Node) : ICommand<CreateNodeResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateNodeResult(NodeId Id);

public class CreateNodeCommandValidator : AbstractValidator<CreateNodeCommand>
{
    public CreateNodeCommandValidator()
    {
        RuleFor(x => x.Node.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Node.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Node.Timestamp)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Timestamp cannot be in the future.");

        RuleFor(x => x.Node.Importance)
            .InclusiveBetween(1, 10).WithMessage("Importance must be between 1 and 10.");

        RuleFor(x => x.Node.Phase)
            .NotEmpty().WithMessage("Phase is required.");

        RuleForEach(x => x.Node.Categories)
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

        RuleForEach(x => x.Node.Tags)
            .MaximumLength(50).WithMessage("Tag must not exceed 50 characters.");
    }
}
