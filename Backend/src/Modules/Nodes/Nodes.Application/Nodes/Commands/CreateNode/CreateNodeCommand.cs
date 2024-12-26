namespace Nodes.Application.Nodes.Commands.CreateNode;

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

        // ToDo: Add remaining Node command validators
    }
}
