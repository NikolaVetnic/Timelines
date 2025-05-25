using BuildingBlocks.Application.Cqrs;
using Nodes.Domain.Models;
using Nodes.Infrastructure.Data;
using Assembly = System.Reflection.Assembly;

namespace Nodes.Architecture;

public abstract class BaseTests
{
    protected static readonly Assembly DomainAssembly = typeof(Node).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(NodesDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
