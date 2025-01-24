// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Core.Api.Sdk.Contracts.Timelines.Dtos;

public class TimelineDto
{
    public string? Id { get; set; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}
