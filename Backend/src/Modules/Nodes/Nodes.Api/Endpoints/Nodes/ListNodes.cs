using System.Collections.Generic;
using BuildingBlocks.Application.Pagination;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Nodes.Application.Entities.Nodes.Dtos;
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

public record ListNodesResponse(PaginatedResult<NodeDto> Nodes);
