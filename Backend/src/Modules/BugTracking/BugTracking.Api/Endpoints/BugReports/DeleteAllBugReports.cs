using System.Collections.Generic;
using BugTracking.Application.Entities.BugReports.Commands.DeleteAllBugReports;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BugTracking.Api.Endpoints.BugReports;

public class DeleteAllBugReports : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/BugReports", async (ISender sender) =>
            {
                var result = await sender.Send(new DeleteAllBugReportsCommand());
                var response = result.Adapt<DeleteAllBugReportsResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteAllBugReports")
            .Produces<DeleteAllBugReportsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete All Bug Reports")
            .WithDescription("Delete All Bug Reports");
    }
}

public record DeleteAllBugReportsResponse(List<string> BugReportIds);
