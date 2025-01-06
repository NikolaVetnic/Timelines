using BoDi;
using Core.Api.Sdk;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Core.Test;

[Binding]
public class SpecFlowDependencies(IObjectContainer container)
{
    [BeforeScenario]
    public void RegisterClient()
    {
        var factory = new WebApplicationFactory<Program>();
        var httpClient = factory.CreateClient();
        var client = new CoreApiClient(httpClient);

        container.RegisterInstanceAs<ICoreApiClient>(client);
    }
}
