namespace Nodes.Application.Entities.Nodes.Commands.UpdateNode;

public record UpdateNodeCommand(NodeDto Node) : ICommand<UpdateNodeResult>;

public record UpdateNodeResult(bool NodeUpdated);

public class UpdateNodeCommandValidator : AbstractValidator<UpdateNodeCommand>
{
    public UpdateNodeCommandValidator()
    {
        RuleFor(x => x.Node.Id)
            .NotEmpty().WithMessage("Id is required.");

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
    }
}
