using BuildingBlocks.Domain.ValueObjects.Ids;
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
    public CreateNodeRequest() { }

    public CreateNodeRequest(NodeDto node) => Node = node;
    
    public NodeDto Node { get; set; }
}

public record CreateNodeResponse(NodeId Id);
