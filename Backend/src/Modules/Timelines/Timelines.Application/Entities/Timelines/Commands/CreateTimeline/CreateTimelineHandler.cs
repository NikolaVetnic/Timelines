namespace Timelines.Application.Entities.Timelines.Commands.CreateTimeline;

internal class CreateTimelineHandler(ITimelinesDbContext dbContext) : ICommandHandler<CreateTimelineCommand, CreateTimelineResult>
{
    public async Task<CreateTimelineResult> Handle(CreateTimelineCommand command, CancellationToken cancellationToken)
    {
        var timeline = command.ToTimeline();

        dbContext.Timelines.Add(timeline);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTimelineResult(timeline.Id);
    }
}

internal static class CreateTimelineCommandExtensions
{
    public static Timeline ToTimeline(this CreateTimelineCommand command)
    {
        return Timeline.Create(
            TimelineId.Of(Guid.NewGuid()),
            command.Timeline.Title
        );
    }
}
