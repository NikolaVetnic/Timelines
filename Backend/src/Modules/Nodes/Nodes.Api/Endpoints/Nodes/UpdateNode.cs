using System;
using System.Collections.Generic;
using Nodes.Application.Entities.Nodes.Commands.UpdateNode;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global

namespace Nodes.Api.Endpoints.Nodes;

public class UpdateNode : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Nodes", async (UpdateNodeRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateNodeCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateNodeResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateNode")
            .Produces<UpdateNodeResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Node")
            .WithDescription("Update Node");
    }
}

public class UpdateNodeRequest
{
    public required string Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime Timestamp { get; init; }
    public int Importance { get; init; }
    public string Phase { get; init; }
    public List<string> Categories { get; init; }
    public List<string> Tags { get; init; }
}

public record UpdateNodeResponse(bool NodeUpdated);
