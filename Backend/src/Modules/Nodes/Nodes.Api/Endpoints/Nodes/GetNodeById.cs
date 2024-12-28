using System.Text.Json.Serialization;
using Nodes.Application.Entities.Nodes.Queries.GetNodeById;

namespace Nodes.Api.Endpoints.Nodes;

// ReSharper disable once UnusedType.Global
public class GetNodeById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Nodes/{nodeId}", async (string nodeId, ISender sender) =>
        {
            var result = await sender.Send(new GetNodeByIdQuery(nodeId));
            var response = result.Adapt<GetNodeByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetNodeById")
        .Produces<GetNodeByIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Node by Id")
        .WithDescription("Get Node by Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetNodeByIdResponse([property: JsonPropertyName("node")] NodeDto NodeDto);
