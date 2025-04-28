using BuildingBlocks.Application.Exceptions.Handlers;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Extensions;
using Carter;
using Core.Api.Extensions;
using Core.Api.Sdk;
using Core.Api.Sdk.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Users.Domain.Models;
using Users.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Clear and reconfigure configuration sources if needed
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.override.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 3;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCarter();
builder.Services.AddModules(builder.Configuration);
builder.Services.AddInterceptors();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

builder.Services.AddHttpClient<ICoreApiClient, CoreApiClient>();

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
