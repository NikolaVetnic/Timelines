// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Core.Api.Sdk.Contracts.Nodes.Dtos;

public class NodeDto
{
    public string? Id { get; set; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTime Timestamp { get; init; }
    public required int Importance { get; init; }
    public required string Phase { get; init; }
    public required List<string> Categories { get; set; }
    public required List<string> Tags { get; set; }
}
