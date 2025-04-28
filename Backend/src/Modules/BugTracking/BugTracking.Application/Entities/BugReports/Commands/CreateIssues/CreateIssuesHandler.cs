using System.Net.Http.Headers;
using System.Net.Http.Json;
using BugTracking.Application.Data;
using BuildingBlocks.Application.Cqrs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BugTracking.Application.Entities.BugReports.Commands.CreateIssues;

public class CreateIssuesHandler(IBugTrackingDbContext dbContext, IConfiguration configuration)
    : ICommandHandler<CreateIssuesCommand, CreateIssuesResult>
{
    public async Task<CreateIssuesResult> Handle(CreateIssuesCommand command, CancellationToken cancellationToken)
    {
        var token = configuration["GitHub:Token"];

        var repoName = configuration["GitHub:Repo:Name"];
        var repoOwner = configuration["GitHub:Repo:Owner"];

        var unprocessedBugReports = await dbContext.BugReports
            .Where(r => r.IsProcessed == false)
            .OrderBy(br => br.CreatedAt)
            .ToListAsync(cancellationToken: cancellationToken);

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("TimelinesBugTracking");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

        var responses = new List<string>();

        foreach (var bugReport in unprocessedBugReports)
        {
            var request = new
            {
                title = bugReport.Title,
                body = $"""
                        **Reported by**: {bugReport.ReporterName}
                        **Reported on**: {bugReport.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC

                        # Description
                        {bugReport.Description}
                        """,
                labels = new[] { "auto-generated", "bug" }
            };

            var response = await httpClient.PostAsJsonAsync(
                $"https://api.github.com/repos/{repoOwner}/{repoName}/issues",
                request,
                cancellationToken);

            bugReport.IsProcessed = response.IsSuccessStatusCode;

            if (bugReport.IsProcessed)
                responses.Add($"{bugReport.Id}");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateIssuesResult(responses);
    }
}
