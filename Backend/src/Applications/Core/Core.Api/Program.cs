using BuildingBlocks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Files.Api.Extensions;
using Nodes.Api.Extensions;
using Notes.Api.Extensions;
using Reminders.Api.Extensions;
using Timelines.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFilesModule();
builder.Services.AddNodesModule();
builder.Services.AddNotesModule();
builder.Services.AddRemindersModule();
builder.Services.AddTimelinesModule();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

var app = builder.Build();

app.UseRouting();

app.MapControllers();
app.MapFilesModuleEndpoints();
app.MapNodesModuleEndpoints();
app.MapNotesModuleEndpoints();
app.MapRemindersModuleEndpoints();
app.MapTimelinesModuleEndpoints();

app.MapGet("/", BuildingBlocksTestClass.GetTestString);

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();