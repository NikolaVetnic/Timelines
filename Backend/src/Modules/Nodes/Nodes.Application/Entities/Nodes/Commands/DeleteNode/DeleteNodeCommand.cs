namespace Nodes.Application.Entities.Nodes.Commands.DeleteNode;

public record DeleteNodeCommand(string Id) : ICommand<DeleteNodeResult>;

public record DeleteNodeResult(bool NodeDeleted);

public class DeleteNodeCommandValidator : AbstractValidator<DeleteNodeCommand>
{
    public DeleteNodeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
