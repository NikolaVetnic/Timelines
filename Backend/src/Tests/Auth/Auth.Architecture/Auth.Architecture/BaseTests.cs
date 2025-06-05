using BuildingBlocks.Application.Cqrs;
using Auth.Domain.Models;
using Auth.Infrastructure.Data;
using Assembly = System.Reflection.Assembly;

namespace Auth.Architecture;

public abstract class BaseTests
{
    protected static readonly Assembly DomainAssembly = typeof(ApplicationUser).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(AuthDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
