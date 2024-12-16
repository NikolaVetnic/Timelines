Infrastructure, Registering the DbContext:
- NodesDbContext in Infrastructure layer, applies configurations from NodeConfiguration which implements IEntityTypeConfiguration<>
- INodesDbContext in Application layer
- Registered in Nodes.Infrastructure.DependencyInjection with AddNodesInfrastructureServices method
- Method AddNodesInfrastructureServices called in Nodes.Api.Extensions.ServiceCollectionExtensions method AddNodesModule
- Method AddNodesModule called in Core.Api.Extensions.ModuleExtensions method AddModules
- Method AddModules called in Core.Api.Program

Infrastructure, Migrate and Seed Db:
- Migration and seeding handled by MigrateAndSeedNodesDatabaseAsync in Nodes.Infrastructure.Data.Extensions.DatabaseExtensions
- InitialData class used when seeding
- Migrations are located in Nodes.Infrastructure.Data.Migrations
- Migration and seeding called by Core.Api.Extensions.DatabaseExtensions method MigrateAndSeedAllModulesAsync
- Core.Api.Program calls Core.Api.Extensions.DatabaseExtensions method MigrateAndSeedAllModulesAsync

Application and Api, Add Services:
- Nodes.Application.Extensions.ServiceCollectionExtensions method AddApplicationServices adds MediatR service (and will add behaviors)
- Nodes.Api.Extensions method AddApiServices adds Carter service (and will add exception handling and health checks)
- Both AddApplicationServices and AddApiServices are called in Nodes.Api.Extensions.ServiceCollectionExtensions method AddNodesModule
- UseNodesModule extension method used for mapping Carter (and will be used for exception handler and health checks)
- Both AddNodeModules and UseNodeModules called in Core.Api.Extensions methods AddModules and UseModules
- Both AddModules and UseModules called in Core.Api.Program