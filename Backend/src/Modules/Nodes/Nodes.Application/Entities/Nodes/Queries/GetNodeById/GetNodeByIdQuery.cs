using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

// ReSharper disable ClassNeverInstantiated.Global

public record GetNodeByIdQuery(NodeId Id) : IQuery<GetNodeByIdResult>
{
    public GetNodeByIdQuery(string Id) : this(NodeId.Of(Guid.Parse(Id))) { }
}

// ReSharper disable once NotAccessedPositionalProperty.Global

public record GetNodeByIdResult(NodeDto NodeDto);

public class GetNodeByIdQueryValidator : AbstractValidator<GetNodeByIdQuery>
{
    public GetNodeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
