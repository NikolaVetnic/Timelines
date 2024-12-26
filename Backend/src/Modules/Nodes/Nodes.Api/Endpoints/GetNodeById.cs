using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Nodes.Application.Dtos;
using Nodes.Application.Nodes.Queries.GetNodeById;

namespace Nodes.Api.Endpoints;

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
public record GetNodeByIdResponse(NodeDto NodeDto);
