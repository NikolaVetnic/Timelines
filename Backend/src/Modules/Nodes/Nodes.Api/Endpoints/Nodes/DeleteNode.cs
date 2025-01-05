using Nodes.Application.Entities.Nodes.Commands.DeleteNode;

namespace Nodes.Api.Endpoints.Nodes;

public class DeleteNode : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Nodes/{nodeId}", async (string nodeId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteNodeCommand(nodeId));
                var response = result.Adapt<DeleteNodeResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteNode")
            .Produces<DeleteNodeResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Node")
            .WithDescription("Delete Node");
    }
}

public record DeleteNodeResponse(bool NodeDeleted);