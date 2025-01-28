using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Nodes.Node.Dtos;
using Nodes.Application.Entities.Nodes.Queries.ListNodes;

namespace Nodes.Api.Endpoints.Nodes;

public class ListNodes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Nodes", async ([AsParameters] PaginationRequest query, ISender sender) =>
        {
            var result = await sender.Send(new ListNodesQuery(query));
            var response = result.Adapt<ListNodesResponse>();

            return Results.Ok(response);
        })
        .WithName("ListNodes")
        .Produces<ListNodesResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("List Nodes")
        .WithDescription("List Nodes");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListNodesResponse(PaginatedResult<NodeDto> Nodes);
