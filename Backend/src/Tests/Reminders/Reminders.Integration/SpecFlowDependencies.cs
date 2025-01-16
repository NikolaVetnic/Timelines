using BoDi;
using Core.Api.Sdk;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Reminders.Integration;

[Binding]
public class SpecFlowDependencies(IObjectContainer container)
{
    [BeforeScenario]
    public void RegisterClient()
    {
        var factory = new WebApplicationFactory<Program>();
        var httpClient = factory.CreateClient();
        var client = new CoreApiNodeClient(httpClient);

        container.RegisterInstanceAs<ICoreApiClient>(client);
    }
}
