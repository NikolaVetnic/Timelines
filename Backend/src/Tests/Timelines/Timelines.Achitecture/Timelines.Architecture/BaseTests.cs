using BuildingBlocks.Application.Cqrs;
using Timelines.Domain.Models;
using Timelines.Infrastructure.Data;
using Assembly = System.Reflection.Assembly;

namespace Timelines.Architecture;

public abstract class BaseTests
{
    protected static readonly Assembly DomainAssembly = typeof(Timeline).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(TimelinesDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
