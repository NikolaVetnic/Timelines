using BuildingBlocks.Application.Cqrs;
using Files.Domain.Models;
using Files.Infrastructure.Data;
using Assembly = System.Reflection.Assembly;

namespace Files.Architecture;

public abstract class BaseTests
{
    protected static readonly Assembly DomainAssembly = typeof(FileAsset).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(FilesDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
