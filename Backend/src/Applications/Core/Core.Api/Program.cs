using System.Net;
using BuildingBlocks.Api.Middlewares;
using BuildingBlocks.Application.Exceptions.Handlers;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Extensions;
using Carter;
using Core.Api.Extensions;
using Core.Api.Sdk;
using Core.Api.Sdk.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Clear and reconfigure configuration sources if needed
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.override.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCarter();
builder.Services.AddModules(builder.Configuration);
builder.Services.AddInterceptors();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

builder.Services.AddHttpClient<ICoreApiClient, CoreApiClient>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            if (allowedOrigins != null)
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
switch (app.Environment.EnvironmentName)
{
    case "Development":
        var devOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            RequireHeaderSymmetry = false,
            ForwardLimit = null
        };

        devOptions.KnownNetworks.Clear();
        devOptions.KnownProxies.Clear();

        app.UseForwardedHeaders(devOptions);

        break;
    case "Production":
        var prodOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            RequireHeaderSymmetry = true,
            ForwardLimit = 1,
            KnownProxies = { IPAddress.Parse("172.19.0.1") } // `docker inspect nginx-proxy | grep -i ipaddress`
        };

        app.UseForwardedHeaders(prodOptions);

        break;
}

app.UseRouting();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseModules();
app.MapCarter();

app.MapGet("/", BuildingBlocksTestClass.GetTestString);

app.UseSwaggerDocumentation(app.Environment);
await app.Services.SetupDatabaseAsync(app.Environment);

app.UseExceptionHandler(_ => { }); // ToDo: To be removed as it eats up any exceptions on startup

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.UseCors("AllowSpecificOrigins");

app.Run();

public abstract partial class Program; // This partial class is needed for the integration tests
