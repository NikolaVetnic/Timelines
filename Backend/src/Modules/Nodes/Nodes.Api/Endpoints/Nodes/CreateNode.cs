using System;
using System.Collections.Generic;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Nodes.Application.Entities.Nodes.Commands.CreateNode;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Nodes.Api.Endpoints.Nodes;

public class CreateNode : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Nodes", async (CreateNodeRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateNodeCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateNodeResponse>();

            return Results.Created($"/Nodes/{response.Id}", response);
        })
        .WithName("CreateNode")
        .Produces<CreateNodeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Node")
        .WithDescription("Creates a new node");
    }
}

public class CreateNodeRequest
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

public record CreateNodeResponse(NodeId Id);
