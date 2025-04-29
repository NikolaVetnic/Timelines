using System;
using System.Collections.Generic;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Entities.Nodes.Commands.UpdateNode;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Nodes.Api.Endpoints.Nodes;

public class UpdateNode : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Nodes/{nodeId}", async (string nodeId, UpdateNodeRequest request, ISender sender) =>
            {
                var command = new UpdateNodeCommand
                {
                    Id = NodeId.Of(Guid.Parse(nodeId)),
                    Title = request.Title,
                    Description = request.Description,
                    Phase = request.Phase,
                    Timestamp = request.Timestamp,
                    Importance = request.Importance,
                    Categories = request.Categories,
                    Tags = request.Tags,
                    TimelineId = request.TimelineId
                };
                
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateNodeResponse>();

                return Results.Ok(response);
            })
        .WithName("UpdateNode")
        .Produces<UpdateNodeResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Node")
        .WithDescription("Updates a node");
    }
}

public class UpdateNodeRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public int Importance { get; set; }
    public string Phase { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public TimelineId TimelineId { get; set; }
}

public record UpdateNodeResponse(NodeBaseDto NodeDto);
