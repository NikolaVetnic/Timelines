// ReSharper disable ClassNeverInstantiated.Global

using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace Timelines.Application.Entities.Timelines.Queries.GetTimelineById;

public record GetTimelineByIdQuery(TimelineId Id) : IQuery<GetTimelineByIdResult>
{
    public GetTimelineByIdQuery(string Id) : this(TimelineId.Of(Guid.Parse(Id))) { }
}

// ReSharper disable once NotAccessedPositionalProperty.Global

public record GetTimelineByIdResult(TimelineDto TimelineDto);

public class GetTimelineByIdQueryValidator : AbstractValidator<GetTimelineByIdQuery>
{
    public GetTimelineByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
