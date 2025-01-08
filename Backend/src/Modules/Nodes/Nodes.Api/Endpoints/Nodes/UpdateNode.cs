using Nodes.Application.Entities.Nodes.Commands.UpdateNode;

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

public record UpdateNodeRequest(NodeDto Node);

public record UpdateNodeResponse(bool NodeUpdated);
