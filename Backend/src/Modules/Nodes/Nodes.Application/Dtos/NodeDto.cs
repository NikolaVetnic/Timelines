namespace Nodes.Application.Dtos;

public record NodeDto(
    string Title,
    string Description,
    DateTime Timestamp,
    int Importance,
    string Phase,
    List<string> Categories,
    List<string> Tags);