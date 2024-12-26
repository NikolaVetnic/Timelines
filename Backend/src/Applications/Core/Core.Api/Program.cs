using BuildingBlocks.Application.Exceptions.Handlers;
using BuildingBlocks.Domain;
using Carter;
using Core.Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddCarter();
builder.Services.AddModules(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();
app.UseModules();
app.MapCarter();

app.UseExceptionHandler(_ => { });

app.MapGet("/", BuildingBlocksTestClass.GetTestString);

// Environment-specific configuration
if (app.Environment.IsDevelopment())
    await app.Services.MigrateAndSeedAllModulesAsync();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
