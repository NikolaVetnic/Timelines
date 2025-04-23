using System.Collections.Generic;
using BugTracking.Application.Entities.BugReports.Queries.ListBugReports;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.BugTracking.BugReport.Dto;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BugTracking.Api.Endpoints.BugReports;

public class ListBugReports : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/BugReports", async (ISender sender) =>
            {
                var result = await sender.Send(new ListBugReportsQuery());
                var response = result.Adapt<ListBugReportsResponse>();

                return Results.Ok(response);
            })
            .WithName("ListBugReports")
            .Produces<ListBugReportsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List Bug Reports")
            .WithDescription("List Bug Reports");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListBugReportsResponse(List<BugReportBaseDto> BugReports);