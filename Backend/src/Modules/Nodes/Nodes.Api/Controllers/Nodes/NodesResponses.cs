using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using System.Text.Json.Serialization;

namespace Nodes.Api.Controllers.Nodes;

public record CreateNodeResponse(NodeId Id);

public record GetNodeByIdResponse([property: JsonPropertyName("node")] NodeDto NodeDto);

public record ListNodesResponse(PaginatedResult<NodeDto> Nodes);