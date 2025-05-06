using System;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;
using Nodes.Application.Entities.Phases.Commands.CreatePhase;

namespace Nodes.Api.Endpoints.Phases;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class CreatePhase : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Phases", async (CreatePhaseRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreatePhaseCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreatePhaseResponse>();

                return Results.Created($"/Phases/{response.Id}", response);
            })
            .WithName("CreatePhase")
            .Produces<CreatePhaseResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Phase")
            .WithDescription("Creates a new phase");
    }
}

public class CreatePhaseRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Duration { get; set; }
    public  string Status { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted { get; set; }
    public PhaseId Parent { get; set; }
    public PhaseId[] DependsOn { get; set; }
    public string AssignedTo { get; set; }
    public string[] Stakeholders { get; set; }
    public string[] Tags { get; set; }
}

public record CreatePhaseResponse(PhaseId Id);
