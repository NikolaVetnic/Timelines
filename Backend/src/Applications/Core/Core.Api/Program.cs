using BuildingBlocks.Application.Exceptions.Handlers;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Extensions;
using Carter;
using Core.Api.Extensions;
using Core.Api.Sdk;
using Core.Api.Sdk.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

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
            policy.WithOrigins(allowedOrigins!)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();
app.UseModules();
app.MapCarter();

app.MapGet("/", BuildingBlocksTestClass.GetTestString);

app.UseSwaggerDocumentation(app.Environment);
await app.Services.MigrateAndSeedAllModulesAsync(app.Environment);

app.UseExceptionHandler(_ => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

public abstract partial class Program; // This partial class is needed for the integration tests
