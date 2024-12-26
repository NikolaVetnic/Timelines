namespace Nodes.Application.Entities.Nodes.Dtos;

public record NodeDto(
    string Id,
    string Title,
    string Description,
    DateTime Timestamp,
    int Importance,
    string Phase,
    List<string> Categories,
    List<string> Tags);
