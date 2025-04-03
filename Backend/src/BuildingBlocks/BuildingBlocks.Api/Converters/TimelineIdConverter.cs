using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace BuildingBlocks.Api.Converters;

public class TimelineIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<TimelineId, TimelineId>().ConstructUsing(src => TimelineId.Of(src.Value));
}
