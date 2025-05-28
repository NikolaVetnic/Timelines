namespace Timelines.Application.Data.Abstractions;

public interface ITimelinesDbContext
{
    DbSet<Timeline> Timelines { get; }
    DbSet<Phase> Phases { get; }
    DbSet<PhysicalPerson> PhysicalPersons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
