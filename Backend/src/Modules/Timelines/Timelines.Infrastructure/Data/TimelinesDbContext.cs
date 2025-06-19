using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using System.Reflection;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using Timelines.Application.Data.Abstractions;
using PhaseId = BuildingBlocks.Domain.Timelines.Phase.ValueObjects.PhaseId;

namespace Timelines.Infrastructure.Data;

public class TimelinesDbContext(DbContextOptions<TimelinesDbContext> options) :
    DbContext(options), ITimelinesDbContext
{
    public DbSet<Timeline> Timelines { get; init; }

    public DbSet<Phase> Phases { get; init; }

    public DbSet<PhysicalPerson> PhysicalPersons { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Timelines");

        // Prevent EF from ever trying to map any Id as an entity
        builder.Ignore<TimelineId>();
        builder.Ignore<PhaseId>();
        builder.Ignore<PhysicalPersonId>();

        // Configure entities
        builder.Entity<Timeline>(entity =>
        {
            entity.ToTable("Timelines"); // Specify table name within the schema

            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired();

            // Map the NodeIds as a collection of IDs
            entity.Ignore(t => t.NodeIds); // This prevents EF from expecting a navigation property
            entity.Property(t => t.Id).ValueGeneratedNever();  // Ensures IDs are managed externally

            entity.Property(t => t.NodeIds)
                .HasConversion(new NodeIdListConverter())
                .HasColumnName("NodeIds")
                .IsRequired(false);

            // Map the PhaseIds as a collection of IDs
            entity.Ignore(t => t.PhaseIds); // This prevents EF from expecting a navigation property
            entity.Property(t => t.Id).ValueGeneratedNever();  // Ensures IDs are managed externally

            entity.Property(t => t.PhaseIds)
                .HasConversion(new PhaseIdListConverter())
                .HasColumnName("PhaseIds")
                .IsRequired(false);
        });

        builder.Entity<Phase>(entity =>
        {
            entity.ToTable("Phases"); // Specify table name within the schema

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasConversion(new PhaseIdValueConverter());

            entity.Property(p => p.TimelineId).IsRequired();
            entity.HasIndex(p => p.TimelineId); // Add an index for efficient querying
            entity.Property(p => p.TimelineId)
                .HasConversion(new TimelineIdValueConverter()) // Apply the value converter
                .IsRequired();

            entity.Property(p => p.Title).IsRequired();
            entity.Property(p => p.Description).IsRequired();
            entity.Property(p => p.StartDate).IsRequired();
            entity.Property(p => p.EndDate).IsRequired(false);
            entity.Property(p => p.Duration).IsRequired(false);
            entity.Property(p => p.Status).IsRequired();
            entity.Property(p => p.Progress).IsRequired();
            entity.Property(p => p.IsCompleted).IsRequired();

            entity.Property(p => p.DependsOn)
                .HasConversion(new DependsOnPhaseIdListConverter())
                .HasColumnName("DependsOn")
                .IsRequired(false);
        });

        builder.Entity<PhysicalPerson>(entity =>
        {
            entity.ToTable("PhysicalPersons");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasConversion(new PhysicalPersonIdValueConverter());

            entity.Property(p => p.TimelineId).IsRequired();
            entity.HasIndex(p => p.TimelineId); // Add an index for efficient querying
            entity.Property(p => p.TimelineId)
                .HasConversion(new TimelineIdValueConverter()) // Apply the value converter
                .IsRequired();

            entity.Property(p => p.FirstName).IsRequired();
            entity.Property(p => p.MiddleName).IsRequired(false);
            entity.Property(p => p.LastName).IsRequired();
            entity.Property(p => p.ParentName).IsRequired();
            entity.Property(p => p.BirthDate).IsRequired();
            entity.Property(p => p.StreetAddress).IsRequired();
            entity.Property(p => p.PersonalIdNumber).IsRequired();
            entity.Property(p => p.IdCardNumber).IsRequired();
            entity.Property(p => p.EmailAddress).IsRequired();
            entity.Property(p => p.PhoneNumber).IsRequired();
            entity.Property(p => p.BankAccountNumber).IsRequired(false);
            entity.Property(p => p.Comment).IsRequired(false);
            entity.Property(p => p.TimelineId).IsRequired();
            entity.Property(p => p.OwnerId).IsRequired();
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

