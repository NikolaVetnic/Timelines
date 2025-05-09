using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using System.Text.Json.Serialization;

namespace Nodes.Api.Controllers.Nodes;

public record CreateNodeResponse(NodeId Id);

public record GetNodeByIdResponse([property: JsonPropertyName("node")] NodeDto NodeDto);

public record ListNodesResponse(PaginatedResult<NodeDto> Nodes);

public record ListFileAssetsByNodeIdResponse(PaginatedResult<FileAssetBaseDto> FileAssets);

public record ListNotesByNodeIdResponse(PaginatedResult<NoteBaseDto> Notes);

public record ListRemindersByNodeIdResponse(PaginatedResult<ReminderBaseDto> Reminders);

public record UpdateNodeResponse(NodeBaseDto Node);
