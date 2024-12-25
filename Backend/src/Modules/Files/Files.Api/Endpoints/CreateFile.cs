using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Files.Api.Endpoints;

public class CreateFile : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Nodes", async (CreateFileRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateFileCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateFileResponse>();

                return Results.Created($"/Nodes/{response.Id}", response);
            })
            .WithName("CreateFile")
            .Produces<CreateFileResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create File")
            .WithDescription("Creates a new file");
    }
}

public record CreateFileRequest(FileDto File);