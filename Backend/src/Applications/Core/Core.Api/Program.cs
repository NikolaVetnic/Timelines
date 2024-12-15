using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Files.Api.Extensions;
using Nodes.Api.Extensions;
using Notes.Api.Extensions;
using Reminders.Api.Extensions;
using Timelines.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddFilesModule();
builder.Services.AddNodesModule(builder.Configuration);
builder.Services.AddNotesModule();
builder.Services.AddRemindersModule();
builder.Services.AddTimelinesModule();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();

app.MapFilesModuleEndpoints();
app.MapNodesModuleEndpoints();
app.MapNotesModuleEndpoints();
app.MapRemindersModuleEndpoints();
app.MapTimelinesModuleEndpoints();

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