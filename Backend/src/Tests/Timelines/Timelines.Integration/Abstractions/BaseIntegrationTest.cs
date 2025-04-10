using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Timelines.Infrastructure.Data;
using Xunit;

namespace Timelines.Integration.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly ISender Sender;
    protected readonly TimelinesDbContext TimelinesDbContext;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        TimelinesDbContext = scope.ServiceProvider.GetRequiredService<TimelinesDbContext>();
        HttpClient = factory.CreateClient(); // ✅ Here’s the line you need
    }
}