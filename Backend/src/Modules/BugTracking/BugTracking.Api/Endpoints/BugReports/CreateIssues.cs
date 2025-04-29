using System.Collections.Generic;
using BugTracking.Application.Entities.BugReports.Commands.CreateIssues;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace BugTracking.Api.Endpoints.BugReports;

public class CreateIssues : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/BugReports/ToIssues", async (ISender sender, HttpContext httpContext,
                IConfiguration configuration) =>
            {
                var providedKey = httpContext.Request.Headers["X-Api-Key"].ToString();
                var expectedKey = configuration["ApiKey"];

                if (providedKey != expectedKey)
                    return Results.Unauthorized();
                
                var result = await sender.Send(new CreateIssuesCommand());
                var response = result.Adapt<CreateIssuesResult>();

                return Results.Created("/BugReports/ToIssues", response);
            })
            .WithName("CreateIssues")
            .Produces<CreateIssuesResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Issues")
            .WithDescription("Creates issues");
    }
}

public record CreateIssuesResponse(List<string> ProcessedBugReportIds);
