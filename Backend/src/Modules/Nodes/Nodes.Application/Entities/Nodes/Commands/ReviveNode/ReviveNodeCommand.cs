using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Nodes.Application.Entities.Nodes.Commands.ReviveNode;

public record ReviveNodeCommand(NodeId Id) : ICommand<ReviveNodeResult>
{
    public ReviveNodeCommand(string Id) : this(NodeId.Of(Guid.Parse(Id))) { }
}

public record ReviveNodeResult(bool NodeRevived);

public class ReviveNodeCommandValidator : AbstractValidator<ReviveNodeCommand>
{
    public ReviveNodeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
