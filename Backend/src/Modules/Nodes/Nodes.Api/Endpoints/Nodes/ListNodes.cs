using System.Collections.Generic;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Nodes.Application.Entities.Nodes.Queries.ListNodes;
using Nodes.Domain.Models;

namespace Nodes.Api.Endpoints.Nodes;

public class ListNodes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Nodes", async ([AsParameters] ListNodesRequest request, ISender sender) =>
        {
            var query = request.Adapt<ListNodesQuery>();
            var result = await sender.Send(query);
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

public record ListNodesRequest(int? PageNumber = 1, int? PageSize = 10);

public record ListNodesResponse(IEnumerable<Node> Nodes);
