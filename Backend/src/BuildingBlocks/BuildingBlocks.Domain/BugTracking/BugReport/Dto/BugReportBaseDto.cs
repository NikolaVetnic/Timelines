using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.BugTracking.BugReport.Dto;

public class BugReportBaseDto(
    string? id,
    string title,
    string description,
    string reporterName)
{
    [JsonPropertyName("id")] public string? Id { get; set; } = id;
    [JsonPropertyName("title")] public string Title { get; set; } = title;
    [JsonPropertyName("description")] public string Description { get; set; } = description;
    [JsonPropertyName("reporterName")] public string ReporterName { get; set; } = reporterName;
}
