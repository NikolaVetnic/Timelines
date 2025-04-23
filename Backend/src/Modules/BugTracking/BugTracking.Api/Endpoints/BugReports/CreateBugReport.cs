using BugTracking.Application.Entities.BugReports.Commands.CreateBugReport;
using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BugTracking.Api.Endpoints.BugReports;

public class CreateBugReport : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/BugReports", async (CreateBugReportRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBugReportCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateBugReportResult>();
        
                return Results.Created($"/Files/{response.Id}", response);
            })
            .WithName("CreateBugReport")
            .Produces<CreateBugReportResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Bug Report")
            .WithDescription("Creates a new bug report");
    }
}

public record CreateBugReportRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ReporterName { get; set; }
}

public record CreateBugReportResponse(BugReportId Id);