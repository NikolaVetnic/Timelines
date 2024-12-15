using BuildingBlocks.Domain.ValueObjects.Ids;
using FluentValidation;
using Nodes.Application.Dtos;

namespace Nodes.Application.Nodes.Commands.CreateNode;

public record CreateNodeCommand(NodeDto Node) : ICommand<CreateNodeResult>;

public record CreateNodeResult(NodeId Id);

public class CreateNodeCommandValidator : AbstractValidator<CreateNodeCommand>
{
    public CreateNodeCommandValidator()
    {
        RuleFor(x => x.Node.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining Node command validators
    }
}