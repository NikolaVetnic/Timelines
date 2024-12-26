using BuildingBlocks.Domain.ValueObjects.Ids;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Nodes.Application.Entities.Nodes.Commands.CreateNode;
using Nodes.Application.Entities.Nodes.Dtos;

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

public record CreateNodeRequest(NodeDto Node);

public record CreateNodeResponse(NodeId Id);
