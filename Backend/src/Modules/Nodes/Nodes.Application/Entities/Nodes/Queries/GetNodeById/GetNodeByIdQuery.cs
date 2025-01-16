// ReSharper disable ClassNeverInstantiated.Global

namespace Nodes.Application.Entities.Nodes.Queries.GetNodeById;

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
