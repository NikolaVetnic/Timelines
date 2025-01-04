namespace Nodes.Application.Entities.Nodes.Commands.DeleteNode;

public record DeleteNodeCommand(string NodeId) : ICommand<DeleteNodeResult>;

public record DeleteNodeResult(bool NodeDeleted);

public class DeleteNodeCommandValidator : AbstractValidator<DeleteNodeCommand>
{
    public DeleteNodeCommandValidator()
    {
        RuleFor(x => x.NodeId).NotEmpty().WithMessage("NodeId is required");
    }
}
