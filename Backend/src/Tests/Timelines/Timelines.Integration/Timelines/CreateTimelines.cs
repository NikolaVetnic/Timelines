using System.Net;
using System.Net.Http.Json;
using Timelines.Api.Endpoints.Timelines;
using Timelines.Integration.Abstractions;
using Xunit;

namespace Timelines.Integration.Timelines;

public class CreateTimelines(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateTimeline_ShouldAdd_NewTimelineToDatabase()
    {
        // Arrange
        var request = new CreateTimelineRequest
        {
            Title = "Endpoint timeline",
            Description = "Created via HTTP endpoint"
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync("/Timelines", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadFromJsonAsync<CreateTimelineResponse>();
        responseBody.Should().NotBeNull();
        responseBody!.Id.Should().NotBeNull();

        var persistedTimeline = TimelinesDbContext.Timelines.FirstOrDefault(t => t.Id == responseBody.Id);
        persistedTimeline.Should().NotBeNull();
        persistedTimeline!.Title.Should().Be(request.Title);
        persistedTimeline.Description.Should().Be(request.Description);
    }
}
