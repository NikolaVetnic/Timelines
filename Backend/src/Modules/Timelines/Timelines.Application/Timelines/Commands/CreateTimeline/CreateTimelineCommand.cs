using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.ValueObjects.Ids;
using FluentValidation;
using Timelines.Application.Dtos;

namespace Timelines.Application.Timelines.Commands.CreateTimeline;

public record CreateTimelineCommand(TimelineDto Timeline) : ICommand<CreateTimelineResult>;

public record CreateTimelineResult(TimelineId Id);

public class CreateTimelineCommandValidator : AbstractValidator<CreateTimelineCommand>
{
    public CreateTimelineCommandValidator()
    {
        RuleFor(x => x.Timeline.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining Timeline command validators
    }
}
