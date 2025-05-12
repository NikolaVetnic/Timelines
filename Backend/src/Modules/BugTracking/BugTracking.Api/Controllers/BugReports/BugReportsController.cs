using System.Threading.Tasks;
using BugTracking.Application.Entities.BugReports.Commands.CreateBugReport;
using BugTracking.Application.Entities.BugReports.Commands.CreateIssues;
using BugTracking.Application.Entities.BugReports.Commands.DeleteAllBugReports;
using BugTracking.Application.Entities.BugReports.Queries.ListBugReports;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BugTracking.Api.Controllers.BugReports;

[ApiController]
[Route("api/[controller]")]
public class BugReportsController(ISender sender, IConfiguration configuration) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateBugReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateBugReportResponse>> CreateBugReport([FromBody] CreateBugReportRequest request)
    {
        var command = request.Adapt<CreateBugReportCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateBugReportResponse>();

        return CreatedAtAction(nameof(CreateBugReport), new { id = response.Id }, response);
    }

    [HttpPost("ToIssues")]
    [ProducesResponseType(typeof(CreateIssuesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateIssuesResponse>> CreateIssues([FromBody] CreateBugReportRequest request)
    {
        var providedKey = HttpContext.Request.Headers["X-Api-Key"].ToString();
        var expectedKey = configuration["ApiKey"];

        if (providedKey != expectedKey)
            return Unauthorized();

        var result = await sender.Send(new CreateIssuesCommand());
        var response = result.Adapt<CreateIssuesResult>();

        return CreatedAtAction(nameof(CreateIssues), response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListBugReportsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListBugReportsResponse>> ListBugReports()
    {
        var result = await sender.Send(new ListBugReportsQuery());
        var response = result.Adapt<ListBugReportsResponse>();

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteAllBugReportsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteAllBugReportsResponse>> Delete()
    {
        var result = await sender.Send(new DeleteAllBugReportsCommand());
        var response = result.Adapt<DeleteAllBugReportsResponse>();

        return Ok(response);
    }
}
