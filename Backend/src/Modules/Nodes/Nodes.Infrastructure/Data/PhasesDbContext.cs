namespace Nodes.Infrastructure.Data;

public class PhasesDbContext(DbContextOptions<PhasesDbContext> options) : DbContext(options), IPhasesDbContext 
{
}
