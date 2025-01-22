// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Core.Api.Sdk.Contracts.Timelines.ValueObjects;

public class TimelineId
{
    public TimelineId() { }

    public TimelineId(Guid value) => Value = value;

    public Guid Value { get; init; }
}
